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

namespace CustomTranslatorCLI.Commands
{
    [Command(Description = "Commands related to project management.")]
    [Subcommand(typeof(Create))]
    [Subcommand(typeof(List))]
    [Subcommand(typeof(Show))]
    [Subcommand(typeof(Delete))]
    class ProjectCommand : TranslatorCommandBase
    {
        public ProjectCommand(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config, IConfiguration appConfiguration) : base(customTranslatorAPI, console, config) { }

        [Command(Description = "Creates new project.")]
        class Create : ParamActionCommandBase
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Workspace ID.")]
            [Guid]
            [Required]
            public string WorkspaceId { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "(Required) Project name.")]
            [MaxLength(256)]
            [Required]
            string Name { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Project description.")]
            [MaxLength(500)]
            string Description { get; set; }

            [Option("-lp|--LanguagePair", CommandOptionType.SingleValue, Description = "Language pair (format xx:yy. eg. en:fr).")]
            [RegularExpression(@"^\w{2}:\w{2}$")]
            [Required]
            string LanguagePair { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Category.")]
            [Required]
            string Category { get; set; }

            [Option("-cd|--CategoryDescriptor", CommandOptionType.SingleValue, Description = "Category descriptor (max 75 chars).")]
            [MaxLength(75)]
            string CategoryDescriptor { get; set; }

            [Option("-label|--Label", CommandOptionType.SingleValue, Description = "Label (max 30 chars).")]
            [MaxLength(30)]
            string Label { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                LanguagePair = LanguagePair.ToLower();

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

                // Get the categories
                var categories = CallApi<IList<TranslatorCategory>>(() => sdk.GetCategories(atc.GetToken()));
                if (categories == null)
                    return -1;

                var categoryId = (from c in categories
                                  where c.Name.ToLower() == Category.ToLower()
                                  select c.Id).FirstOrDefault();

                if (categoryId == 0)
                {
                    console.WriteLine("Invalid or unsupported Category.");
                    return -1;
                }
                
                // Populate the new project data
                var projectDefinition = new CreateProjectData()
                {
                    LanguagePairId = (int)languagePairId.Value,
                    CategoryId = (int)categoryId,
                    Name = Name,
                    CategoryDescriptor = CategoryDescriptor,
                    Description = Description,
                    Label = Label
                };

                if (!Json.HasValue)
                {
                    console.WriteLine("Creating project...");
                }
                sdk.CreateProject(projectDefinition, atc.GetToken(), WorkspaceId);

                var res1 = CallApi<ProjectsResponse>(() => sdk.GetProjects(atc.GetToken(), WorkspaceId, 1));
                if (res1 == null)
                    return -1;

                if (res1.Projects.Count == 0)
                {
                    if (!Json.HasValue)
                    {
                        console.WriteLine("No projects found.");
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
                        foreach (var project in res1.Projects)
                        {
                            if (project.Name == Name)
                            {
                                console.WriteLine($"{project.Id,30} {project.Name,-25}");
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

        [Command(Description = "Lists projects in your workspace.")]
        class List
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) Workspace ID.")]
            [Guid]
            [Required]
            public string WorkspaceId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Getting projects...");
                }

                var res = CallApi<ProjectsResponse>(() => sdk.GetProjects(atc.GetToken(), WorkspaceId, 1));
                if (res == null)
                    return -1;

                if (res.Projects.Count == 0)
                {
                    if (!Json.HasValue)
                    {
                        console.WriteLine("No projects found.");
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
                        foreach (var project in res.Projects)
                        {
                            console.WriteLine($"{project.Id,30} {project.Name,-25}");
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

        [Command(Description = "Shows details of specified project.")]
        class Show
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) ID of the project to show.")]
            [Guid]
            [Required]
            public string ProjectId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Getting project...");
                }

                ProjectInfo res;
                try
                {
                    res = CallApi<ProjectInfo>(() => sdk.GetProjectById(new Guid(ProjectId), atc.GetToken()));
                    if (res == null)
                        return -1;
                }
                catch (HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Project ID: project not found.");
                        return -1;
                    }
                    throw;
                }

                console.WriteLine(SafeJsonConvert.SerializeObject(res, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));

                return 0;
            }
        }

        [Command(Description = "Deletes specified project.")]
        class Delete
        {
            [Option(CommandOptionType.SingleValue, Description = "(Required) ID of the project to delete.")]
            [Guid]
            [Required]
            public string ProjectId { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Return output as JSON.")]
            bool? Json { get; set; }

            int OnExecute(IConsole console, IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk, IAccessTokenClient atc)
            {
                if (!Json.HasValue)
                {
                    console.WriteLine("Deleting project...");
                }
                
                try
                {
                    sdk.DeleteProject(new Guid(ProjectId), atc.GetToken());
                }
                catch (HttpOperationException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    {
                        console.WriteLine("Invalid Project ID: project not found.");
                        return -1;
                    }
                    throw;
                }
                if (!Json.HasValue)
                {
                    console.WriteLine("Done.");
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
