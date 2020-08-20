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
using System.Text;
using System.Threading;
using Xunit;

namespace Azure_Custom_Translator_CLI.Tests.CommandTests
{
    public class ProjectCommandTests
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
        public void List_Json_Success()
        {
            // ARRANGE
            var response = new ProjectsResponse()
            {
                Projects = new List<ProjectInfo>()
                {
                    new ProjectInfo()
                    {
                        Id = Guid.Empty,
                        Name = "Moq"
                    }
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetProjectsWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), null, null, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ProjectsResponse>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("project list -ws 00000000-0000-0000-0000-000000000000 -j");
            app.Execute(args);

            // ASSESS
            string expected = @"{
  ""projects"": [
    {
      ""id"": ""00000000-0000-0000-0000-000000000000"",
      ""name"": ""Moq"",
      ""label"": null,
      ""description"": null,
      ""languagePair"": null,
      ""category"": null,
      ""categoryDescriptor"": null,
      ""baselineBleuScorePunctuated"": null,
      ""bleuScorePunctuated"": null,
      ""baselineBleuScoreUnpunctuated"": null,
      ""bleuScoreUnpunctuated"": null,
      ""baselineBleuScoreCIPunctuated"": null,
      ""bleuScoreCIPunctuated"": null,
      ""baselineBleuScoreCIUnpunctuated"": null,
      ""bleuScoreCIUnpunctuated"": null,
      ""status"": null,
      ""modifiedDate"": ""0001-01-01T00:00:00"",
      ""createdDate"": ""0001-01-01T00:00:00"",
      ""createdBy"": null,
      ""modifiedBy"": null,
      ""apiDomain"": null,
      ""isAvailable"": false,
      ""hubCategory"": null
    }
  ],
  ""pageIndex"": 0,
  ""totalPageCount"": 0
}
";
            string actual = ((MockTestWriter)app.Out).ReadAsString();
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Delete_Json()
        {
            // ARRANGE
 
            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.DeleteProjectWithHttpMessagesAsync(It.IsAny<Guid>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("project delete -p 00000000-0000-0000-0000-000000000000 -j");
            app.Execute(args);

            // ASSESS
            string expected = $@"{{{app.Out.NewLine}  ""status"": ""success""{app.Out.NewLine}}}{app.Out.NewLine}";
            string actual = ((MockTestWriter)app.Out).ReadAsString();
            Assert.Equal(expected, actual);
        }
    }
}
