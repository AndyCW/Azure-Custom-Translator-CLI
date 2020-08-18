using System.Runtime.CompilerServices;
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
using Microsoft.Rest;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomTranslatorCLI.Commands
{
    [Command(Description = "Commands related to model management.")]
    [Subcommand(typeof(Create))]
    [Subcommand(typeof(Deploy))]
    [Subcommand(typeof(List))]
    [Subcommand(typeof(Show))]
    [Subcommand(typeof(Train))]
    [Subcommand(typeof(Delete))]
    class ModelCommand : TranslatorCommandBase
    {
        public ModelCommand(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config, IConfiguration appConfiguration) : base(customTranslatorAPI, console, config) { }

        [Command(Description = "Creates new model.")]
        class Create : ParamActionCommandBase
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Project ID.")]
            [Guid]
            [Required]
            public string ProjectId { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "(Required) Model name (max 100 chars).")]
            [MaxLength(100)]
            [Required]
            string Name { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "(Required) Document IDs (comma separated).")]
            [Required]
            string DocumentIDs { get; set; }

            
            [Option(CommandOptionType.NoValue, Description = "Initiate training.")]
            bool? Train { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                // Populate the new model data
                var docIDs = new List<long?>();
                foreach(var docId in DocumentIDs.Split(','))
                {
                    docIDs.Add(Int64.Parse(docId));
                }
                var modelDefinition = new CreateModelRequest()
                {
                    Name = Name,
                    DocumentIds = docIDs,
                    IsTuningAuto = true,
                    IsTestingAuto = true,
                    IsAutoDeploy = true,
                    IsAutoTrain = Train.HasValue ? true : false,
                    ProjectId = new Guid(ProjectId)
                };

                if (!Json.HasValue)
                {
                    console.WriteLine("Creating model...");
                }

                try
                {
                    sdk.CreateModel(modelDefinition, atc.GetToken());
                }
                catch(HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Project ID: project not found.");
                        return -1;
                    }
                    else if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorDetails = JObject.Parse(ex.Response.Content);
                        console.WriteLine("Error: " + (string)errorDetails["message"]);
                        return -1;
                    }
                    throw;
                }

                var res1 = CallApi<ModelsResponse>(() => sdk.GetProjectsByIdModels(new Guid(ProjectId), atc.GetToken(), 1));
                if (res1 == null)
                    return -1;

                if (res1.Models.Count == 0)
                {
                    if (!Json.HasValue)
                    {
                        console.WriteLine("No models found.");
                    }
                    else
                    {
                        console.WriteLine(SafeJsonConvert.SerializeObject(res1, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                    }
                }
                else
                {
                    if (!Json.HasValue)
                    {
                        foreach (var model in res1.Models)
                        {
                            if (model.Name == Name)
                            {
                                console.WriteLine($"{model.Id,-10} {model.Name,-50} {model.ModelStatus}");
                            }
                        }
                    }
                    else
                    {
                        console.WriteLine(SafeJsonConvert.SerializeObject(res1, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                    }
                }

                return 0;
           }
        }

        [Command(Description = "Deploy or undeploy a model.")]
        class Deploy
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Model ID.")]
            [Required]
            public string ModelId { get; set; }

            [Option("-na|--NorthAmerica", CommandOptionType.SingleValue, Description = "North America region - set to 'true' to deploy (default), 'false' to undeploy.")]
            public bool? NorthAmerica { get; set; }     

            [Option("-eu|--Europe", CommandOptionType.SingleValue, Description = "North America region - set to 'true' to deploy (default), 'false' to undeploy.")]
            public bool? Europe { get; set; }          

            [Option("-as|--AsiaPacific", CommandOptionType.SingleValue, Description = "North America region - set to 'true' to deploy (default), 'false' to undeploy.")]
            public bool? Asia { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Starting model deployment...");
                }
                   
                var regions = new List<ModelRegionStatus>();
                regions.Add(new ModelRegionStatus() 
                {
                    Region = 1,
                    IsDeployed = !NorthAmerica.HasValue ? true : NorthAmerica.Value
                });        
                regions.Add(new ModelRegionStatus() 
                {
                    Region = 2,
                    IsDeployed = !Europe.HasValue ? true : Europe.Value
                });        
                regions.Add(new ModelRegionStatus() 
                {
                    Region = 3,
                    IsDeployed = !Asia.HasValue ? true : Asia.Value
                });        
                try
                {
                    sdk.DeployModel(Int64.Parse(ModelId), atc.GetToken(), regions);
                }
                catch(HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Model ID: model not found.");
                        return -1;
                    }
                    else if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorDetails = JObject.Parse(ex.Response.Content);
                        console.WriteLine("Error: " + (string)errorDetails["message"]);
                        return -1;
                    }
                    throw;
                }

                if (!Json.HasValue)
                {
                    console.WriteLine("Deployment request submitted.");
                }
                else
                {
                    console.WriteLine(SafeJsonConvert.SerializeObject(new { status = "submitted" }, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }
 
                return 0;
            }
        }

        [Command(Description = "Lists all the models for the given project.")]
        class List
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Project ID.")]
            [Guid]
            [Required]
            public string ProjectId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Getting models...");
                }

                var res = CallApi<ModelsResponse>(() => sdk.GetProjectsByIdModels(new Guid(ProjectId), atc.GetToken(), 1));
                if (res == null)
                    return -1;

                if (res.Models.Count == 0)
                {
                    if (!Json.HasValue)
                    {
                        console.WriteLine("No models found.");
                    }
                    else
                    {
                        console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                    }
                }
                else
                {
                    if (!Json.HasValue)
                    {
                        foreach (var model in res.Models)
                        {
                            console.WriteLine($"{model.Id,-10} {model.Name,-50} {model.ModelStatus}");
                        }
                    }
                    else
                    {
                        console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                    }
                }

                return 0;
            }
        }

        [Command(Description = "Shows details of specified model.")]
        class Show
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) ID of the model to show.")]
            [Required]
            public string ModelId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Getting model...");
                }

                try
                {
                    var res = CallApi<ModelInfo>(() => sdk.GetModel(Int64.Parse(ModelId), atc.GetToken()));
                    if (res == null)
                        return -1;

                    console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }
                catch(HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Model ID: model not found.");
                        return -1;
                    }
                    throw;
                }   
                return 0;
            }
        }

        [Command(Description = "Train a model.")]
        class Train
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) ID of the model to train.")]
            [Required]
            public string ModelId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Starting model training...");
                }
                                
                try
                {
                    sdk.TrainModel(Int64.Parse(ModelId), atc.GetToken());
                }
                catch(HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Model ID: model not found.");
                        return -1;
                    }
                    throw;
                }           

                if (!Json.HasValue)
                {
                    console.WriteLine("Training submitted.");
                }
                else
                {
                    console.WriteLine(SafeJsonConvert.SerializeObject(new { status = "submitted" }, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }
                return 0;
            }
        }

        [Command(Description = "Deletes specified model.")]
        class Delete
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) ID of the model to delete.")]
            [Required]
            public string ModelId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Deleting model...");
                }

                try
                {
                    sdk.DeleteModel(Int64.Parse(ModelId), atc.GetToken());
                }
                catch(HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Model ID: model not found.");
                        return -1;
                    }
                    throw;
                }
                if (!Json.HasValue)
                {
                    console.WriteLine("Success.");
                }
                else
                {
                    console.WriteLine(SafeJsonConvert.SerializeObject(new { status = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                }

                return 0;
            }
        }
    }
}
