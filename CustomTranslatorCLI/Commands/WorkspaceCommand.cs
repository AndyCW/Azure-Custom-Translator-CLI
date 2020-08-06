using CustomTranslator;
using CustomTranslatorCLI.Interfaces;
using CustomTranslator.Models;
using CustomTranslatorCLI.Attributes;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CustomTranslatorCLI.Commands
{
    [Command(Description = "Commands related to workspace management.")]
    [Subcommand(typeof(Create))]
    [Subcommand(typeof(List))]
    [Subcommand(typeof(Show))]
    [Subcommand(typeof(Delete))]
    class WorkspaceCommand : TranslatorCommandBase
    {
        public WorkspaceCommand(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config, IConfiguration appConfiguration) : base(customTranslatorAPI, console, config) { }

        [Command(Description = "Creates new workspace.")]
        class Create : ParamActionCommandBase
        {
           [Option(Description = "(Required) workspace name.")]
           [Required]
           string Name { get; set; }

           [Option(Description = "workspace description.")]
           string Description { get; set; }

           int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk)
           {
               var workspaceDefinition = new CreateWorkspaceData()
               {
                    Name = Name,
                    Subscription = new Subscription()
                    {
                        SubscriptionKey = config.TranslatorKey,
                        BillingRegionCode = config.TranslatorRegion
                    }
                };

                console.WriteLine("Creating workspace...");
                sdk.CreateWorkspace(workspaceDefinition, GetBearerToken(appConfiguration));

                var res1 = CallApi<List<WorkspaceInfo>>(() => sdk.GetWorkspaces(GetBearerToken(appConfiguration)));
                if (res1 == null)
                    return -1;

                if (res1.Count == 0)
                {
                    console.WriteLine("No workspaces found.");
                }
                else
                {
                    foreach (var workspace in res1)
                    {
                        if (workspace.Name == Name)
                        {
                            console.WriteLine($"{workspace.Id, 30} {workspace.Name, -25}");
                        }
                    }
                }

                return 0;
           }
        }

        [Command(Description = "Lists workspaces in your subscription.")]
        class List
        {
            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk)
            {
                console.WriteLine("Getting workspaces...");

                var res = CallApi<List<WorkspaceInfo>>(() => sdk.GetWorkspaces(GetBearerToken(appConfiguration)));
                if (res == null)
                    return -1;

                if (res.Count == 0)
                {
                    console.WriteLine("No workspaces found.");
                }
                else
                {
                    foreach (var workspace in res)
                    {
                        console.WriteLine($"{workspace.Id, 30} {workspace.Name, -25}");
                    }
                }

                return 0;
            }
        }

        [Command(Description = "Shows details of specified workspace.")]
        class Show
        {
            [Argument(0, Name = "GUID", Description = "(Required) ID of the workspace to show.")]
            [Guid]
            [Required]
            public string Id { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk)
            {
                console.WriteLine("Getting workspace...");

                var res = CallApi<WorkspaceInfo>(() => sdk.GetWorkspaceById(Id, GetBearerToken(appConfiguration)));
                if (res == null)
                    return -1;

                console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));

                return 0;
            }
        }

        [Command(Description = "Deletes specified workspace.")]
        class Delete
        {
            [Argument(0, Name ="GUID", Description = "(Required) ID of the workspace to delete.")]
            [Guid]
            [Required]
            public string Id { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk)
            {
                console.WriteLine("Deleting workspace...");
                CallApi<ErrorContent>(() => sdk.DeleteWorkspace(Id, GetBearerToken(appConfiguration)));
                console.WriteLine("Done.");

                return 0;
            }
        }
    }
}
