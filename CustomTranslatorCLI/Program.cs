using CustomTranslatorCLI.Interfaces;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest.Serialization;
using CustomTranslator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CustomTranslatorCLI.Helpers;

namespace CustomTranslatorCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfig();

            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.TranslatorKey);
            if (config.TranslatorRegion.ToLower() != "global")
            {
                hc.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", config.TranslatorRegion);
            }

            var sdk = new MicrosoftCustomTranslatorAPIPreview10(hc, true);
            sdk.BaseUri = new Uri($"https://custom-api.cognitive.microsofttranslator.com");

            IConfiguration appConfiguration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            CommandLineApplication<MainApp> app = new CommandLineApplication<MainApp>();
            app.HelpOption();
            app.VersionOptionFromAssemblyAttributes(typeof(Program).Assembly);
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(GetServices(config, appConfiguration, sdk));

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
                Directory.CreateDirectory(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".translator"));
                File.WriteAllText(Config.CONFIG_FILENAME, "[]");
            }

            var config = SafeJsonConvert.DeserializeObject<List<Config>>(File.ReadAllText(Config.CONFIG_FILENAME)).FirstOrDefault(c => c.Selected == true);
            if (config == null)
            {
                config = new Config("Anonymous", "", "global");
            }

            return config;
        }

        static ServiceProvider GetServices(IConfig config, IConfiguration appConfiguration, IMicrosoftCustomTranslatorAPIPreview10 sdk)
        {
            var services = new ServiceCollection()
                .AddSingleton<IConfig>(config)
                .AddSingleton<IConfiguration>(appConfiguration)
                .AddSingleton<IMicrosoftCustomTranslatorAPIPreview10>(sdk)
                .AddSingleton<IAccessTokenClient, AccessTokenClient>()
                .AddSingleton<ICachePersistence, AzureKeyVaultCachePersistence>()
                .BuildServiceProvider();

            return services;
        }
    }
}
