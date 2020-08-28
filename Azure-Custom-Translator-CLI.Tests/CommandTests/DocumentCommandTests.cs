using Azure_Custom_Translator_CLI.Tests.Utils;
using CustomTranslator;
using CustomTranslator.Models;
using CustomTranslatorCLI;
using CustomTranslatorCLI.Interfaces;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Xunit;

namespace Azure_Custom_Translator_CLI.Tests.CommandTests
{
    public class DocumentCommandTests
    {
        public CommandLineApplication<MainApp> InitApp(IMicrosoftCustomTranslatorAPIPreview10 apiObject)
        {
            var services = new ServiceCollection()
                .AddSingleton<IConfig, Config>()
                .AddSingleton<IConfiguration>(new ConfigurationBuilder().Build())
                .AddSingleton<IMicrosoftCustomTranslatorAPIPreview10>(apiObject)
                .AddSingleton<IAccessTokenClient>(new MockAccessTokenClient())
                .BuildServiceProvider();

            var writer = new MockTestWriter();
            var app = new CommandLineApplication<MainApp>(new MockConsole(writer));
            app.Conventions.UseDefaultConventions().UseConstructorInjection(services);

            return app;
        }

        static string[] CommandIntoArgs(string command)
        {
            return command.Split(' ');
        }


        [Fact]
        public void List_Documents()
        {
            // ARRANGE
            var response1 = new DocumentsResponse()
            {
                PaginatedDocuments = new DocumentInfoResponse()
                {
                    PageIndex = 1,
                    TotalPageCount = 2,
                    Documents = new List<DocumentInfo>()
                    {
                        new DocumentInfo()
                        {
                            Name = "firstdoc",
                            Id = 1,
                            Languages = new List<TranslatorLanguage>()
                            {
                                new TranslatorLanguage()
                                {
                                    DisplayName = "Test",
                                    LanguageCode = "ab",
                                    Id = 255
                                }
                            }
                        },
                        new DocumentInfo()
                        {
                            Name = "seconddoc",
                            Id = 2,
                            Languages = new List<TranslatorLanguage>()
                            {
                                new TranslatorLanguage()
                                {
                                    DisplayName = "Test",
                                    LanguageCode = "cd",
                                    Id = 255
                                }
                            }
                        }
                        
                    }
                }
            };
            var response2 = new DocumentsResponse()
            {
                PaginatedDocuments = new DocumentInfoResponse()
                {
                    PageIndex = 2,
                    TotalPageCount = 2,
                    Documents = new List<DocumentInfo>()
                    {
                        new DocumentInfo()
                        {
                            Name = "thirddoc",
                            Id = 3,
                            Languages = new List<TranslatorLanguage>()
                            {
                                new TranslatorLanguage()
                                {
                                    DisplayName = "Test",
                                    LanguageCode = "ef",
                                    Id = 255
                                }
                            }
                        },
                        new DocumentInfo()
                        {
                            Name = "fourthdoc",
                            Id = 4,
                            Languages = new List<TranslatorLanguage>()
                            {
                                new TranslatorLanguage()
                                {
                                    DisplayName = "Test",
                                    LanguageCode = "gh",
                                    Id = 255
                                }
                            }
                        }
                    }
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetDocumentsWithHttpMessagesAsync(string.Empty, 1, It.IsAny<string>(), null, null, null, null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<DocumentsResponse>() { Body = response1 }
                );
            mock
                .Setup(
                    m => m.GetDocumentsWithHttpMessagesAsync(string.Empty, 2, It.IsAny<string>(), null, null, null, null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<DocumentsResponse>() { Body = response2 }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs($"document list -ws 00000000-0000-0000-0000-000000000000");
            app.Execute(args);

            // ASSESS
            string expectedResult = @"Getting documents...
1 ab firstdoc
2 cd seconddoc
3 ef thirddoc
4 gh fourthdoc
";

            Assert.Equal(expectedResult, ((MockTestWriter)app.Out).ReadAsString());
        }
        [Fact]
        public void Upload_LanguagePair_Invalid()
        {
            // ARRANGE

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("document upload -ws 00000000-0000-0000-0000-000000000000 -lp abc:xyz -dt training");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"The field --LanguagePair must match the regular expression '^\w{{2}}:\w{{2}}$'.{ app.Out.NewLine}Specify --help for a list of available options and commands.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }


        [Fact]
        public void Upload_DocumentType_Invalid()
        {
            // ARRANGE

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt test");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"(Required) Document type: one of Training|Testing|Tuning|Phrase|Sentence{ app.Out.NewLine}Specify --help for a list of available options and commands.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Upload_File_Does_not_exist()
        {
            // ARRANGE

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -s abc10.txt");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"The file 'abc10.txt' does not exist.{ app.Out.NewLine}Specify --help for a list of available options and commands.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Upload_TargetFile_Missing()
        {
            // ARRANGE
            var response = new List<LanguagePair>()
            {
                new LanguagePair()
                {
                    SourceLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test",
                        LanguageCode = "ab",
                        Id = 255
                    },
                    TargetLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test2",
                        LanguageCode = "yz",
                        Id = 254
                    },
                    Id = 1
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetSupportedLanguagePairsWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<IList<LanguagePair>>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var file = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "abc.txt");
            var args = CommandIntoArgs($"document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -s {file}");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"--SourceFile and --TargetFile must be specified together.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Upload_SourceFile_Missing()
        {
            // ARRANGE
            var response = new List<LanguagePair>()
            {
                new LanguagePair()
                {
                    SourceLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test",
                        LanguageCode = "ab",
                        Id = 255
                    },
                    TargetLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test2",
                        LanguageCode = "yz",
                        Id = 254
                    },
                    Id = 1
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetSupportedLanguagePairsWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<IList<LanguagePair>>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var file = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "abc.txt");
            var args = CommandIntoArgs($"document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -t {file}");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"--SourceFile and --TargetFile must be specified together.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Upload_ComboFile_And_SourceFile_Combined()
        {
            // ARRANGE
            var response = new List<LanguagePair>()
            {
                new LanguagePair()
                {
                    SourceLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test",
                        LanguageCode = "ab",
                        Id = 255
                    },
                    TargetLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test2",
                        LanguageCode = "yz",
                        Id = 254
                    },
                    Id = 1
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetSupportedLanguagePairsWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<IList<LanguagePair>>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var cfile = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "abc.txt");
            var sfile = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "xyz.txt");
            var args = CommandIntoArgs($"document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -c {cfile} -s {sfile}");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"--ComboFile and --SourceFile cannot be specified together.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }


        [Fact]
        public void Upload_ComboFile_And_TargetFile_Combined()
        {
            // ARRANGE
            var response = new List<LanguagePair>()
            {
                new LanguagePair()
                {
                    SourceLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test",
                        LanguageCode = "ab",
                        Id = 255
                    },
                    TargetLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test2",
                        LanguageCode = "yz",
                        Id = 254
                    },
                    Id = 1
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetSupportedLanguagePairsWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<IList<LanguagePair>>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var cfile = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "abc.txt");
            var sfile = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "xyz.txt");
            var args = CommandIntoArgs($"document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -c {cfile} -t {sfile}");
            app.Execute(args);

            // ASSESS
            Assert.Equal(@$"--ComboFile and --TargetFile cannot be specified together.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }


        [Fact]
        public void Upload_Document()
        {
            // ARRANGE
            var response = new List<LanguagePair>()
            {
                new LanguagePair()
                {
                    SourceLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test",
                        LanguageCode = "ab",
                        Id = 255
                    },
                    TargetLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test2",
                        LanguageCode = "yz",
                        Id = 254
                    },
                    Id = 1
                }
            };
            var uploadDocumentResponse = new ImportFilesResponse()
            {
                JobId = Guid.Empty,
                FilesAcceptedForProcessing = new List<string>()
                {
                    { "efg.xlsx" }
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetSupportedLanguagePairsWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<IList<LanguagePair>>() { Body = response }
                );
            mock
                .Setup(
                    m => m.ImportDocumentsWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ImportFilesResponse>() { Body = uploadDocumentResponse }
                );

            var app = InitApp(mock.Object);

            // ACT
            var file = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "abc.txt");
            var args = CommandIntoArgs($"document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -c {file}");
            app.Execute(args);

            // ASSESS
            string expectedResult = @"Uploading documents...
{
  ""jobId"": ""00000000-0000-0000-0000-000000000000"",
  ""filesAcceptedForProcessing"": [
    ""efg.xlsx""
  ]
}
";
            Assert.Equal(expectedResult, ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Upload_Document_Wait()
        {
            // ARRANGE
            var response = new List<LanguagePair>()
            {
                new LanguagePair()
                {
                    SourceLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test",
                        LanguageCode = "ab",
                        Id = 255
                    },
                    TargetLanguage = new TranslatorLanguage()
                    {
                        DisplayName = "Test2",
                        LanguageCode = "yz",
                        Id = 254
                    },
                    Id = 1
                }
            };
            var uploadDocumentResponse = new ImportFilesResponse()
            {
                JobId = Guid.Empty,
                FilesAcceptedForProcessing = new List<string>()
                {
                    { "efg.xlsx" }
                }
            };
            var jobStatusResponse1 = new ImportJobStatusResponse()
            {
                FileProcessingStatus = new List<ImportJobFileStatusInfo>()
                {
                    new ImportJobFileStatusInfo()
                    {
                        DocumentName = "efg.xlsx",
                        Status = new ImportJobStatus()
                        {
                            DisplayName = "processing"
                        }
                    }
                }
            };
            var jobStatusResponse2 = new ImportJobStatusResponse()
            {
                FileProcessingStatus = new List<ImportJobFileStatusInfo>()
                {
                    new ImportJobFileStatusInfo()
                    {
                        DocumentName = "efg.xlsx",
                        Status = new ImportJobStatus()
                        {
                            DisplayName = "Failed"
                        }
                    }
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetSupportedLanguagePairsWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<IList<LanguagePair>>() { Body = response }
                );
            mock
                .Setup(
                    m => m.ImportDocumentsWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ImportFilesResponse>() { Body = uploadDocumentResponse }
                );
            mock
                .Setup(
                    m => m.GetImportJobsByJobIdWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ImportJobStatusResponse>() { Body = jobStatusResponse1 }
                );
            mock
                .Setup(
                    m => m.GetImportJobsByJobIdWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ImportJobStatusResponse>() { Body = jobStatusResponse1 }
                );
            mock
                .Setup(
                    m => m.GetImportJobsByJobIdWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ImportJobStatusResponse>() { Body = jobStatusResponse1 }
                );
            mock
                .Setup(
                    m => m.GetImportJobsByJobIdWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ImportJobStatusResponse>() { Body = jobStatusResponse2 }
                );

            var app = InitApp(mock.Object);

            // ACT
            var file = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "abc.txt");
            var args = CommandIntoArgs($"document upload -ws 00000000-0000-0000-0000-000000000000 -lp ab:yz -dt training -c {file} -w -j");
            app.Execute(args);

            // ASSESS
            string expectedResult = @"
{
  ""jobName"": null,
  ""fileProcessingStatus"": [
    {
      ""status"": {
        ""displayName"": ""Failed"",
        ""id"": 0
      },
      ""modifiedDate"": null,
      ""fileName"": null,
      ""documentName"": ""efg.xlsx"",
      ""summary"": null,
      ""id"": null,
      ""parentId"": null,
      ""language"": null
    }
  ],
  ""pageIndex"": 0,
  ""totalPageCount"": 0
}
";
            Assert.Equal(expectedResult, ((MockTestWriter)app.Out).ReadAsString());
        }

    }
}
