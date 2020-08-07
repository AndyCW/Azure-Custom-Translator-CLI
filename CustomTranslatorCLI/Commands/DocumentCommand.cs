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
            [Option(CommandOptionType.SingleValue, Description = "(Required) Workspace ID.")]
            [Guid]
            [Required]
            public string WorkspaceId { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                console.WriteLine("Getting projects...");

                var res = CallApi<ProjectsResponse>(() => sdk.GetProjects(atc.GetToken(), WorkspaceId, 1));
                if (res == null)
                    return -1;

                if (res.Projects.Count == 0)
                {
                    console.WriteLine("No projects found.");
                }
                else
                {
                    foreach (var project in res.Projects)
                    {
                        console.WriteLine($"{project.Id, 30} {project.Name, -25}");
                    }
                }

                return 0;
            }
        }

        [Command(Description = "Shows status of document import.")]
        class Status
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) ID of the project to show.")]
            [Guid]
            [Required]
            public string ProjectId { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                console.WriteLine("Getting project...");

                var res = CallApi<ProjectInfo>(() => sdk.GetProjectById(new Guid(ProjectId), atc.GetToken()));
                if (res == null)
                    return -1;

                console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));

                return 0;
            }
        }

        [Command(Description = "Uploads a combo document or a parallel document pair.")]
        class Upload
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Workspace ID.")]
            [Guid]
            [Required]
            public string WorkspaceId { get; set; }

            [Option("-dt|--DocumentType", CommandOptionType.SingleValue, Description = "(Required) Document type (Training|Testing|Tuning|Phrase|Sentence).")]
            [Required]
            public string DocumentType { get; set; }

            [Option("-lp|--LanguagePair", CommandOptionType.SingleValue, Description = "Language pair (format xx->yy. eg. en->fr).")]
            [Required]
            string LanguagePair { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Path of Combo file (.TMX|.XLF|.XLIFF|.LCL|.XLSX|.ZIP file required).")]
            public string ComboFile { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Path of source file [Parallel] (.TXT|.HTML.|.HTM|.PDF|.DOCX|.ALIGN file required).")]
            public string SourceFile { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Path of target file [Parallel] (.TXT|.HTML.|.HTM|.PDF|.DOCX|.ALIGN file required).")]
            public string TargetFile { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Document details of the files being uploaded.")]
            [Required]
            public string DocumentDetails { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                                LanguagePair = LanguagePair.ToLower();
                // Validate language pair param
                var regex = @"^\w{2}->\w{2}$";
                var match = Regex.Match(LanguagePair, regex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    console.WriteLine("Invalid LanguagePair, use format en:fr.");
                    return -1;
                }

                // Get the supported language pairs
                var languagePairs = CallApi<IList<LanguagePair>>(() => sdk.GetSupportedLanguagePairs(atc.GetToken()));
                if (languagePairs == null)
                    return -1;

                var languagePairId = (from lp in languagePairs
                    where lp.SourceLanguage.LanguageCode == LanguagePair.Split("->")[0]
                        && (lp.TargetLanguage.LanguageCode == LanguagePair.Split("->")[1])
                    select lp.Id).FirstOrDefault();

                if (languagePairId == null)
                {
                    console.WriteLine("Invalid or unsupported LanguagePair.");
                    return -1;
                }

                console.WriteLine("Uploading documents...");
                sdk.DeleteProject(new Guid(ProjectId), atc.GetToken());
                console.WriteLine("Done.");

                return 0;
            }
        }
    }
}
