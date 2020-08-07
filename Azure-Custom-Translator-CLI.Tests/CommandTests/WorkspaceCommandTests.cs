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
    public class WorkspaceCommandTests
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
        public void List_Success()
        {
            // ARRANGE
            var response = new List<WorkspaceInfo>()
            {
                new WorkspaceInfo()
                {
                    Id = Guid.Empty.ToString(),
                    Name = "Moq"
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetWorkspacesWithHttpMessagesAsync(string.Empty, null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<List<WorkspaceInfo>>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("workspace list");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Getting workspaces...{app.Out.NewLine}00000000-0000-0000-0000-000000000000 {"Moq",-25}{app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }
    }
}
