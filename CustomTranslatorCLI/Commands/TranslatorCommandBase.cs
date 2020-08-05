using CustomTranslator;
using CustomTranslator.Models;
using McMaster.Extensions.CommandLineUtils;
using CustomTranslatorCLI.Interfaces;
using CustomTranslatorCLI.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CustomTranslatorCLI.SDK.Models;
using System.Security.Authentication;
using CustomTranslatorCLI.Helpers;
using Microsoft.Extensions.Configuration;

namespace CustomTranslatorCLI.Commands
{
    [HelpOption("--help")]
    abstract class TranslatorCommandBase
    {
        protected static IMicrosoftCustomTranslatorAPIPreview10 _customTranslatorAPI;
        protected static IConsole _console;
        protected static IConfig _config;

        public TranslatorCommandBase(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config)
        {
            _customTranslatorAPI = customTranslatorAPI;
            _console = console;
            _config = config;
        }

        protected void OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
        }

        protected static string mBearerToken;

        protected static string GetBearerToken(IConfiguration appconfig)
        {
            if (!string.IsNullOrEmpty(mBearerToken))
            {
                return mBearerToken;
            }
            else
            {
                //var res = CallApi<string>(() => customTranslatorAPI.GetAuthToken(config.TranslatorKey, config.TranslatorRegion));
                //if (res == null)
                //    return string.Empty;

                var res = new AccessTokenClient(appconfig).GetToken();

                mBearerToken = "Bearer " + res;
                return mBearerToken;
            }
        }


        /// <summary>
        /// Call API with method returning meaningful object for success and ErrorContent for failure.
        /// </summary>
        /// <exception cref="Exception">When the result of API call is ErrorContent.</exception>
        protected static T CallApi<T>(Func<object> method)
        {
            var res = method.Invoke();
            if (res != null && res is ErrorContent)
            {
                if ((res as ErrorContent).Code == "Unauthorized")
                {
                    _console.Error.WriteLine("Run 'config set' and add your Translator key or select proper configuration set by calling 'config select <name>'.");
                }
                throw new Exception($"API call ended with error: {(res as ErrorContent).Message}");
            }

            return (T)res;
        }

        protected static int CreateAndWait(Func<object> operation, bool wait, Func<Guid, object> probe)
        {
            var res = CallApi<object>(operation);
            if (res is Guid)
            {
                if (wait)
                    return WaitForProcessing((Guid)res, probe);
                else
                    _console.WriteLine("Created.");
                return 0;
            }
            else
            {
                return -1;
            }
        }

        protected static int WaitForProcessing(Guid id, Func<Guid, object> probe)
        {
            _console.Write("Processing [.");
            var done = false;
            while (!done)
            {
                _console.Write(".");
                Thread.Sleep(1000);
                var resource = probe.Invoke(id);
                if (resource is Entity)
                {
                    if ((resource as Entity).Status == Constants.FAILED_STATUS)
                    {
                        _console.WriteLine(".]");
                        _console.Error.WriteLine("Processing failed.");
                        return -1;
                    }

                    done = (resource as Entity).Status == Constants.SUCCEEDED_STATUS;
                }
                else
                {
                    // přišel ErrorResult nebo něco jiného
                    _console.Error.WriteLine("Unable to get status. " + (resource as ErrorContent).Message);
                    return -1;
                }

            }
            _console.WriteLine(".] Done");
            _console.WriteLine(id.ToString());
            return 0;
        }
    }
}
