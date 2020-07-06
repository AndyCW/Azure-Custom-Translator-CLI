﻿using McMaster.Extensions.CommandLineUtils;
using Microsoft.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomTranslatorCLI.Commands
{
    [Command(Description = "Mandatory configuration. Use your Translator API key and region obtained from Azure portal or portal.customtranslator.azure.ai portal.")]
    [Subcommand(typeof(Set))]
    [Subcommand(typeof(List))]
    [Subcommand(typeof(Select))]
    [Subcommand(typeof(Delete))]
    class ConfigCommand
    {
        void OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
        }

        [Command(Description = "Sets speech API key.")]
        class Set : ParamActionCommandBase
        {
            [Option(Description = "Name of the config set. If  Creates new one if not found, otherwise updates existing one.")]
            [Required]
            string Name { get; set; }

            [Option(CommandOptionType.SingleValue, Description = "Translator API subscription key.")]
            string Key { get; set; }

            //[Option(CommandOptionType.SingleValue, Description = "Translator API region. Default: westus")]
            //string Region { get; set; }

            [Option(CommandOptionType.NoValue, Description = "Make this subscription active after storing settings.")]
            bool? Select { get; set; }

            int OnExecute(IConsole console)
            {
                List<Config> configs;
                if (File.Exists(Config.CONFIG_FILENAME))
                    configs = SafeJsonConvert.DeserializeObject<List<Config>>(File.ReadAllText(Config.CONFIG_FILENAME));
                else
                    configs = new List<Config>();

                var config = configs.FirstOrDefault(c => c.Name == Name);
                if (config == null)
                {
                    //config = new Config(Name, defaultRegion: "westus");
                    config = new Config(Name);
                    configs.Add(config);
                }

                config.TranslatorKey = Key ?? config.TranslatorKey;
                //config.TranslatorRegion = Region ?? config.TranslatorRegion;
                if (Select == true) ConfigCommand.Select.ChangeSelection(Name, configs);

                if (string.IsNullOrWhiteSpace(config.TranslatorKey))
                {
                    console.WriteLine("Error: Translator API key is required.");
                    return -1;
                }

                //console.WriteLine($"Setting {Name}: Key = {Key ?? "(no change)"}, Region = {Region ?? "(no change)"}");
                console.WriteLine($"Setting {Name}: Key = {Key ?? "(no change)"}");
                File.WriteAllText(Config.CONFIG_FILENAME, SafeJsonConvert.SerializeObject(configs, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));

                return 0;
            }
        }

        [Command(Description = "Shows current settings.")]
        class List
        {
            int OnExecute(IConsole console, CommandLineApplication app)
            {
                List<Config> configs;
                if (File.Exists(Config.CONFIG_FILENAME))
                {
                    configs = SafeJsonConvert.DeserializeObject<List<Config>>(File.ReadAllText(Config.CONFIG_FILENAME)).OrderByDescending(c => c.Selected).ToList();
                    foreach (var config in configs)
                    {
                        console.WriteLine("Configuration set:");
                        console.WriteLine($"- Name: {config.Name}");
                        console.WriteLine($"- Key: {config.TranslatorKey}");
                        //console.WriteLine($"- Region: {config.TranslatorRegion}");
                        console.WriteLine($"- Selected: {config.Selected}");
                        console.WriteLine();
                    }
                }
                else
                {
                    console.WriteLine("No configuration sets created yet. Please specify settings by calling 'config set'.");
                }

                return 0;
            }
        }

        [Command(Description = "Selects which config set is currently active. Use this command to switch between subscriptions.")]
        class Select : ParamActionCommandBase
        {
            [Argument(0, Description = "Configuration set name. This will be selected for all API operations. Case sensitive.")]
            [Required]
            string Name { get; set; }

            int OnExecute(IConsole console)
            {
                List<Config> configs;
                if (File.Exists(Config.CONFIG_FILENAME))
                {
                    configs = SafeJsonConvert.DeserializeObject<List<Config>>(File.ReadAllText(Config.CONFIG_FILENAME)).OrderByDescending(c => c.Selected).ToList();
                    //foreach (var config in configs)
                    //{
                    //    config.Selected = (config.Name == Name);
                    //}
                    ChangeSelection(Name, configs);

                    File.WriteAllText(Config.CONFIG_FILENAME, SafeJsonConvert.SerializeObject(configs, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                    console.WriteLine($"Configuration set {Name} selected.");
                }
                else
                {
                    console.WriteLine("No configuration sets created yet. Please specify settings by calling 'config set'.");
                }

                return 0;
            }

            /// <summary>
            /// Modifies collection passed as parameter.
            /// </summary>
            public static void ChangeSelection(string newSelectedName, List<Config> configs)
            {
                var nameFound = false;

                // go through all to both select and deselect
                foreach (var config in configs)
                {
                    config.Selected = (config.Name == newSelectedName);
                    if (config.Selected) nameFound = true;
                }

                if (!nameFound)
                    throw new ArgumentOutOfRangeException(nameof(newSelectedName), $"Config name {newSelectedName} was not found in the list.");
            }
        }

        [Command(Description = "Removes selected configuration set.")]
        class Delete : ParamActionCommandBase
        {
            [Argument(0, Description = "Configuration set name.")]
            [Required]
            string Name { get; set; }

            int OnExecute(IConsole console)
            {
                if (File.Exists(Config.CONFIG_FILENAME))
                {
                    var configs = SafeJsonConvert.DeserializeObject<List<Config>>(File.ReadAllText(Config.CONFIG_FILENAME)).OrderByDescending(c => c.Selected).ToList();
                    var config = configs.FirstOrDefault(c => c.Name == Name);
                    if (config == null)
                    {
                        console.WriteLine("Config set not found.");
                        return -1;
                    }

                    configs.Remove(config);
                    var next = configs.FirstOrDefault();
                    if (next != null) next.Selected = true;

                    File.WriteAllText(Config.CONFIG_FILENAME, SafeJsonConvert.SerializeObject(configs, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }));
                    console.WriteLine($"Configuration set {Name} deleted.");
                }
                else
                {
                    console.WriteLine("No configuration sets created yet. Please specify settings by calling 'config set'.");
                }

                return 0;
            }

        }
    }
}
