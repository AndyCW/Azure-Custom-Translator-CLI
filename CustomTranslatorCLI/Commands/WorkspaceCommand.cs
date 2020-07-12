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

namespace CustomTranslatorCLI.Commands
{
    [Command(Description = "Commands related to workspace management.")]
//    [Subcommand(typeof(Create))]
    [Subcommand(typeof(List))]
    [Subcommand(typeof(Show))]
    [Subcommand(typeof(Status))]
    [Subcommand(typeof(Delete))]
    class WorkspaceCommand : TranslatorCommandBase
    {
        public WorkspaceCommand(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config) : base(customTranslatorAPI, console, config) { }

        //[Command(Description = "Creates new workspace.")]
        //class Create : ParamActionCommandBase
        //{
        //    [Option(Description = "(Required) workspace name.")]
        //    [Required]
        //    string Name { get; set; }

        //    [Option(Description = "workspace description.")]
        //    string Description { get; set; }

        //    int OnExecute()
        //    {
        //        var workspaceDefinition = new TextTranslatorModelsRequestTextTranslatorCreateWorkspaceRequest()
        //        {
        //            Name = Name,
        //            Subscription = new TextTranslatorModelsRequestTextTranslatorSubscriptionRequest()
        //            {
        //                SubscriptionKey = _config.TranslatorKey,
        //                BillingRegionCode = _config.TranslatorRegion
        //            }
        //        };

        //        _console.WriteLine("Creating workspace...");
        //        var res = CreateAndWait(
        //            () => _customTranslatorAPI.ApiTexttranslatorV10WorkspacesPost(workspaceDefinition, BearerToken),
        //            true,
        //            _customTranslatorAPI.ApiTexttranslatorV10WorkspacesGet(BearerToken));

        //        return res;
        //    }
        //}

        [Command(Description = "Lists workspaces in your subscription.")]
        class List
        {
            async Task<int> OnExecute(IConsole console, IConfig config, IMicrosoftCustomTranslatorAPIPreview10 sdk)
            {
                console.WriteLine("Getting workspaces...");

                var res1 = await sdk.AuthTokenGetWithHttpMessagesAsync(config.TranslatorKey, config.TranslatorRegion);

                var res = CallApi<List<TextTranslatorApiModelsTextTranslatorWorkspaceInfo>>(() => _customTranslatorAPI.ApiTexttranslatorV10WorkspacesGet(BearerToken));
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

            int OnExecute(IConsole console)
            {
                console.WriteLine("Getting workspace...");

                var res = CallApi<TextTranslatorApiModelsTextTranslatorWorkspaceInfo>(() => _customTranslatorAPI.ApiTexttranslatorV10WorkspacesByIdGet(Id, BearerToken));
                if (res == null)
                    return -1;

                console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));

                return 0;
            }
        }

        [Command(Description = "Shows status of specific workspace.")]
        class Status
        {
            [Argument(0, Name = "GUID", Description = "(Required) workspace ID.")]
            [Required]
            [Guid]
            string Id { get; set; }

            int OnExecute(IConsole console)
            {
                var res = CallApi<TextTranslatorApiModelsTextTranslatorWorkspaceInfo>(() => _customTranslatorAPI.ApiTexttranslatorV10WorkspacesByIdGet(Id, BearerToken));
                if (res == null)
                    return -1;

                console.WriteLine($"{res.Id,30} {res.Name,-25}");

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

            int OnExecute(IConsole console)
            {
                console.WriteLine("Deleting workspace...");
                CallApi<ErrorContent>(() => _customTranslatorAPI.ApiTexttranslatorV10WorkspacesByIdDelete(Id, BearerToken));
                console.WriteLine("Done.");

                return 0;
            }
        }
    }
}
