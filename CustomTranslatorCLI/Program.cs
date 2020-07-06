using CustomTranslatorCLI.Interfaces;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest.Serialization;
using Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CustomTranslatorCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfig();

            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.TranslatorKey);

            var sdk = new MicrosoftCustomTranslatorAPIPreview10(hc, true);
            sdk.BaseUri = new Uri($"https://api/texttranslator/v1.0");

            CommandLineApplication<MainApp> app = new CommandLineApplication<MainApp>();
            app.HelpOption();
            app.VersionOptionFromAssemblyAttributes(typeof(Program).Assembly);
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(GetServices(config, sdk));

            try
            {
                app.Execute(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.InnerException?.Message);
            }
        }

        static Config GetConfig()
        {
            if (!File.Exists(Config.CONFIG_FILENAME))
            {
                Directory.CreateDirectory(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".speech"));
                File.WriteAllText(Config.CONFIG_FILENAME, "[]");
            }

            var config = SafeJsonConvert.DeserializeObject<List<Config>>(File.ReadAllText(Config.CONFIG_FILENAME)).FirstOrDefault(c => c.Selected == true);
            if (config == null)
            {
                config = new Config("Anonymous", "", "northeurope");
            }

            return config;
        }

        static ServiceProvider GetServices(IConfig config, IMicrosoftCustomTranslatorAPIPreview10 sdk)
        {
            var services = new ServiceCollection()
                .AddSingleton<IConfig>(config)
                .AddSingleton<IMicrosoftCustomTranslatorAPIPreview10>(sdk)
                .BuildServiceProvider();

            return services;
        }
    }
}
