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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Xunit;

namespace Azure_Custom_Translator_CLI.Tests.CommandTests
{
    public class ModelCommandTest
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
        public void Create_Model()
        {
            // ARRANGE
            var response = new ModelsResponse()
            {
                Models = new List<ModelInfo>()
                {
                    new ModelInfo()
                    {
                        Name = "testmodel",
                        Id = 1234,
                        ModelStatus = "draft"
                    }
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.CreateModelWithHttpMessagesAsync(It.IsAny<CreateModelRequest>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );

            mock
                .Setup(
                    m => m.GetProjectsByIdModelsWithHttpMessagesAsync(new Guid("00000000-0000-0000-0000-000000000000"), It.IsAny<string>(), 1, It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(new HttpOperationResponse<ModelsResponse>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model create -p 00000000-0000-0000-0000-000000000000 -n testmodel -d 1,2");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Creating model...{ app.Out.NewLine}1234       testmodel                                          draft{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Create_Model_Wait_Json()
        {
            // ARRANGE
            var response1 = new ModelsResponse()
            {
                Models = new List<ModelInfo>()
                {
                    new ModelInfo()
                    {
                        Name = "testmodel",
                        Id = 1234,
                        ModelStatus = "draft"
                    }
                }
            };
            var response2 = new ModelInfo()
            {
                Name = "testmodel",
                Id = 1234,
                ModelStatus = "draft"
            };
            var response3 = new ModelInfo()
            {
                Name = "testmodel",
                Id = 1234,
                ModelStatus = "draft"
            }; 
            var response4 = new ModelInfo()
            {
                Name = "testmodel",
                Id = 1234,
                ModelStatus = "trained"
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.CreateModelWithHttpMessagesAsync(It.IsAny<CreateModelRequest>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );
            mock
                .Setup(
                    m => m.GetProjectsByIdModelsWithHttpMessagesAsync(new Guid("00000000-0000-0000-0000-000000000000"), It.IsAny<string>(), 1, It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(new HttpOperationResponse<ModelsResponse>() { Body = response1 }
                );
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response2 }
                );
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response3 }
                );
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response4 }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model create -p 00000000-0000-0000-0000-000000000000 -n testmodel -d 1,2 -t -w -j");
            app.Execute(args);

            // ASSESS
            string expected = @"
{
  ""id"": 1234,
  ""name"": ""testmodel"",
  ""modelIdentifier"": null,
  ""projectId"": ""00000000-0000-0000-0000-000000000000"",
  ""documents"": null,
  ""modelRegionStatuses"": null,
  ""baselineBleuScorePunctuated"": null,
  ""bleuScorePunctuated"": null,
  ""baselineBleuScoreUnpunctuated"": null,
  ""bleuScoreUnpunctuated"": null,
  ""baselineBleuScoreCIPunctuated"": null,
  ""bleuScoreCIPunctuated"": null,
  ""baselineBleuScoreCIUnpunctuated"": null,
  ""bleuScoreCIUnpunctuated"": null,
  ""startDate"": null,
  ""completionDate"": null,
  ""modifiedDate"": ""0001-01-01T00:00:00"",
  ""createdDate"": ""0001-01-01T00:00:00"",
  ""createdBy"": null,
  ""modifiedBy"": null,
  ""trainingSentenceCount"": null,
  ""tuningSentenceCount"": null,
  ""testingSentenceCount"": null,
  ""phraseDictionarySentenceCount"": null,
  ""sentenceDictionarySentenceCount"": null,
  ""monolingualSentenceCount"": null,
  ""modelStatus"": ""trained"",
  ""statusInfo"": null,
  ""isTuningAuto"": false,
  ""isTestingAuto"": false,
  ""isAutoDeploy"": false,
  ""autoDeployThreshold"": 0.0,
  ""hubBLEUScore"": null,
  ""hubCategory"": null,
  ""errorCode"": null
}
";
            Assert.Equal(expected, ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Deploy_Model()
        {
            // ARRANGE

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.DeployModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<List<ModelRegionStatus>>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model deploy -m 1234 -na true -eu false");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Starting model deployment...{ app.Out.NewLine}Deployment request submitted.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void List_Model()
        {
            // ARRANGE
            var response = new ModelsResponse()
            {
                Models = new List<ModelInfo>()
                {
                    new ModelInfo()
                    {
                        Name = "testmodel",
                        Id = 1234
                    }
                }
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetProjectsByIdModelsWithHttpMessagesAsync(It.IsAny<Guid>(), It.IsAny<string>(), 1, It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelsResponse>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model list -p 00000000-0000-0000-0000-000000000000");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Getting models...{ app.Out.NewLine}1234       testmodel                                          { app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Show_Model()
        {
            // ARRANGE
            var response = new ModelInfo()
            {
                Name = "testmodel",
                Id = 1234
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response }
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model show -m 1234");
            app.Execute(args);

            // ASSESS
            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Assert.Equal($"Getting model...{ app.Out.NewLine}{json}{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Train_Model()
        {
            // ARRANGE

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.TrainModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model train -m 1234");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Starting model training...{ app.Out.NewLine}Training submitted.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }


        [Fact]
        public void Train_Model_Wait()
        {
            // ARRANGE
            var response1 = new ModelInfo()
            {
                Name = "testmodel",
                Id = 1234,
                ModelStatus = "draft"
            };
            var response2 = new ModelInfo()
            {
                Name = "testmodel",
                Id = 1234,
                ModelStatus = "trained"
            };

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.TrainModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response1 }
                );
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response1 }
                );
            mock
                .Setup(
                    m => m.GetModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse<ModelInfo>() { Body = response2 }
                ); 
            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model train -m 1234 -w");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Starting model training...{ app.Out.NewLine}Processing [...] Done{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }

        [Fact]
        public void Delete_Model()
        {
            // ARRANGE

            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.DeleteModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .ReturnsAsync(
                    new HttpOperationResponse()
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model delete -m 1234");
            app.Execute(args);

            // ASSESS
            Assert.Equal($"Deleting model...{ app.Out.NewLine}Success.{ app.Out.NewLine}", ((MockTestWriter)app.Out).ReadAsString());
        }


        [Fact]
        public void Invalid_Model()
        {
            // ARRANGE
            var mock = new Mock<IMicrosoftCustomTranslatorAPIPreview10>();
            mock
                .Setup(
                    m => m.TrainModelWithHttpMessagesAsync(It.IsAny<long>(), It.IsAny<string>(), null, CancellationToken.None)
                    )
                .Throws(new HttpOperationException() { Response = new HttpResponseMessageWrapper(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound), string.Empty)}
                );

            var app = InitApp(mock.Object);

            // ACT
            var args = CommandIntoArgs("model train -m 1234");
            Exception ex = Assert.Throws<Exception>(() => app.Execute(args));

            // ASSESS
            Assert.Equal($"Invalid ID: target entity not found.", ex.Message);
        }
    }
}
