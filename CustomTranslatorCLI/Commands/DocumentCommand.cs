using System.Text.RegularExpressions;
using CustomTranslator;
using CustomTranslatorCLI.Interfaces;
using CustomTranslator.Models;
using CustomTranslatorCLI.Attributes;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CustomTranslatorCLI.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace CustomTranslatorCLI.Commands
{
    [Command(Description = "Commands related to document management.")]
    [Subcommand(typeof(List))]
    [Subcommand(typeof(Status))]
    [Subcommand(typeof(Upload))]
    class DocumentCommand : TranslatorCommandBase
    {
        public DocumentCommand(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config, IConfiguration appConfiguration) : base(customTranslatorAPI, console, config) { }

        [Command(Description = "Lists documents in your workspace.")]
        class List
        {
            [Option("-ws|--Workspace", CommandOptionType.SingleValue, Description = "(Required) Workspace ID.")]
            [Guid]
            [Required]
            public string WorkspaceId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Getting documents...");
                }

                int pageIndex = 1;
                List<DocumentInfo> documents = new List<DocumentInfo>();

                while (true)
                {
                    var res = CallApi<DocumentsResponse>(() => sdk.GetDocuments(atc.GetToken(), pageIndex, WorkspaceId));
                    if (res == null)
                        throw new Exception("GetDocuments returned null response");

                    documents.AddRange(res.PaginatedDocuments.Documents);

                    pageIndex++;
                    if (pageIndex > res.PaginatedDocuments.TotalPageCount)
                    {
                        break;
                    }
                }

                //Report results
                if (!Json.HasValue)
                {
                    foreach (var document in documents)
                    {
                        console.WriteLine($"{document.Id} {document.Languages[0].LanguageCode} {document.Name}");
                    }
                }
                else
                {
                    console.WriteLine(SafeJsonConvert.SerializeObject(documents, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }

                return 0;
            }
        }

        [Command(Description = "Shows status of document import.")]
        class Status
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Job ID to show.")]
            [Guid]
            [Required]
            public string JobId { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                console.WriteLine("Getting Import Status...");

                int pageIndex = 1;
                List<ImportJobFileStatusInfo> jobStatus = new List<ImportJobFileStatusInfo>();

                while (true)
                {
                    var res = CallApi<ImportJobStatusResponse>(() => sdk.GetImportJobsByJobId(atc.GetToken(), new Guid(JobId), pageIndex, 100));
                    if (res == null)
                        throw new Exception("GetImportJobsByJobId returned null response");

                    jobStatus.AddRange(res.FileProcessingStatus);

                    pageIndex++;
                    if (pageIndex > res.TotalPageCount)
                    {
                        break;
                    }
                }

                console.WriteLine(SafeJsonConvert.SerializeObject(jobStatus, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));

                return 0;
            }
        }

        [Command(Description = "Uploads a combo document or a parallel document pair. Usage: translator document upload -w {workspaceId} -dt {documentType} -lp {languagePair} -c {comboFilePath} [-o] *OR* translator document upload -w {workspaceId} -dt {documentType} -lp {languagePair} -s {sourceFilePath} -t {targetFiePath} -pn {name} [-o]")]
        class Upload
        {
            [Option("-ws|--Workspace", CommandOptionType.SingleValue, Description = "(Required) Workspace ID.")]
            [Guid]
            [Required]
            public string WorkspaceId { get; set; }

            [Option("-dt|--DocumentType", CommandOptionType.SingleValue, Description = "(Required) Document type (Training|Testing|Tuning|Phrase|Sentence).")]
            [ListValidator("training|testing|tuning|phrase|sentence", ErrorMessage= "(Required) Document type: one of Training|Testing|Tuning|Phrase|Sentence")]
            [Required]
            public string DocumentType { get; set; }

            [Option("-lp|--LanguagePair", CommandOptionType.SingleValue, Description = "Language pair (format xx:yy. eg. en:fr).")]
            [RegularExpression(@"^\w{2}:\w{2}$")]
            [Required]
            string LanguagePair { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Path of Combo file (.TMX|.XLF|.XLIFF|.LCL|.XLSX|.ZIP file required).")]
            [FileExists]
            public string ComboFile { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Path of source file [Parallel] (.TXT|.HTML.|.HTM|.PDF|.DOCX|.ALIGN file required).")]
            [FileExists]
            public string SourceFile { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Path of target file [Parallel] (.TXT|.HTML.|.HTM|.PDF|.DOCX|.ALIGN file required).")]
            [FileExists]
            public string TargetFile { get; set; }

            [Option("-pn|--ParallelName", CommandOptionType.SingleValue, Description = "Document name of parallel file set.")]
            [MaxLength(255)]
            public string ParallelName { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Override document if it exists.")]
            bool? Override { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Will stop and wait for upload completion.")]
            bool Wait { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                _sdk = sdk;
                _atc = atc;
                _console = console;

                // Get the supported language pairs
                var languagePairs = CallApi<IList<LanguagePair>>(() => sdk.GetSupportedLanguagePairs(atc.GetToken()));
                if (languagePairs == null)
                    return -1;

                var languagePairId = (from lp in languagePairs
                    where lp.SourceLanguage.LanguageCode == LanguagePair.Split(":")[0]
                        && (lp.TargetLanguage.LanguageCode == LanguagePair.Split(":")[1])
                    select lp.Id).FirstOrDefault();

                if (languagePairId == null)
                {
                    console.WriteLine("Invalid or unsupported LanguagePair.");
                    return -1;
                }

                // Validate arg combinations
                if (!string.IsNullOrEmpty(ComboFile) && !string.IsNullOrEmpty(SourceFile))
                {
                    console.WriteLine("--ComboFile and --SourceFile cannot be specified together.");
                    return -1;
                }
                if (!string.IsNullOrEmpty(ComboFile) && !string.IsNullOrEmpty(TargetFile))
                {
                    console.WriteLine("--ComboFile and --TargetFile cannot be specified together.");
                    return -1;
                }
                if (!string.IsNullOrEmpty(SourceFile) && string.IsNullOrEmpty(TargetFile))
                {
                    console.WriteLine("--SourceFile and --TargetFile must be specified together.");
                    return -1;
                }
                if (string.IsNullOrEmpty(SourceFile) && !string.IsNullOrEmpty(TargetFile))
                {
                    console.WriteLine("--SourceFile and --TargetFile must be specified together.");
                    return -1;
                }
                if (!string.IsNullOrEmpty(SourceFile) && !string.IsNullOrEmpty(TargetFile) && string.IsNullOrEmpty(ParallelName))
                {
                    console.WriteLine("--ParallelName must be specified with --SourceFile and --TargetFile.");
                    return -1;
                }

                // Build request data
                if (!Json.HasValue)
                {
                    console.WriteLine("Uploading documents...");
                }

                var documentDetails = new DocumentDetailsForImportRequest()
                {
                    DocumentName = ParallelName,
                    DocumentType = DocumentTypeLookup.Types[DocumentType.ToLowerInvariant()],
                    FileDetails = new List<FileForImportRequest>()
                };

                if (!string.IsNullOrEmpty(ComboFile))
                {
                    documentDetails.FileDetails.Add(new FileForImportRequest()
                    {
                        Name = Path.GetFileName(ComboFile),
                        LanguageCode = LanguagePair.Split(":")[1],
                        OverwriteIfExists = Override.HasValue ? true : false
                    });                    
                    documentDetails.IsParallel = false;
                }
                else
                {
                    documentDetails.FileDetails.Add(new FileForImportRequest()
                    {
                        Name = Path.GetFileName(SourceFile),
                        LanguageCode = LanguagePair.Split(":")[0],
                        OverwriteIfExists = Override.HasValue ? true : false
                    });
                    documentDetails.FileDetails.Add(new FileForImportRequest()
                    {
                        Name = Path.GetFileName(TargetFile),
                        LanguageCode = LanguagePair.Split(":")[1],
                        OverwriteIfExists = Override.HasValue ? true : false
                    });
                    documentDetails.IsParallel = true;
                }
                var details = new List<DocumentDetailsForImportRequest>() { documentDetails };

                var files = string.Empty;
                if (!string.IsNullOrEmpty(ComboFile))
                {
                    files = ComboFile;
                }
                else
                {
                    files = SourceFile + "|" + TargetFile;
                }

                var importFilesJobResponse = CallApi<ImportFilesResponse>(() => sdk.ImportDocuments(atc.GetToken(), files , JsonConvert.SerializeObject(details, Formatting.Indented), WorkspaceId));
                if (importFilesJobResponse == null)
                    return -1;

                if (!Wait)
                {
                    console.WriteLine(SafeJsonConvert.SerializeObject(importFilesJobResponse, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }
                else
                {
                    Guid jobId = importFilesJobResponse.JobId.Value;
                    CreateAndWait(() => sdk.GetImportJobsByJobId(atc.GetToken(), jobId, 1, 100), jobId, true, (jobId) => IsUploaded(jobId), Json.HasValue ? false : true);

                    var jobStatusResponse = CallApi<ImportJobStatusResponse>(() => sdk.GetImportJobsByJobId(atc.GetToken(), jobId, 1, 100));
                    console.WriteLine(SafeJsonConvert.SerializeObject(jobStatusResponse, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }

                return 0;
            }

            bool IsUploaded(Guid jobId)
            {
                var statusValues = new List<string>()
                {
                    "Succeeded",
                    "Failed"
                };
                var jobStatusResponse = _sdk.GetImportJobsByJobId(_atc.GetToken(), jobId, 1, 100);
                int isCompleteCount = 0;
                foreach (var fileStatus in jobStatusResponse.FileProcessingStatus)
                {
                    if (statusValues.Contains(fileStatus.Status.DisplayName))
                    {
                        isCompleteCount++;
                    }
                }
                return isCompleteCount >= jobStatusResponse.FileProcessingStatus.Count;
            }
        }
    }
}
