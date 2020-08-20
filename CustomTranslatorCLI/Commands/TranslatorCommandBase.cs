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
using System.Runtime.CompilerServices;
using Microsoft.Rest;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CustomTranslatorCLI.Commands
{
    [HelpOption("--help")]
    abstract class TranslatorCommandBase
    {
        protected static IMicrosoftCustomTranslatorAPIPreview10 _sdk;
        protected static IConsole _console;
        protected static IConfig _config;
        protected static IAccessTokenClient _atc;

        public TranslatorCommandBase(IMicrosoftCustomTranslatorAPIPreview10 customTranslatorAPI, IConsole console, IConfig config)
        {
            _sdk = customTranslatorAPI;
            _console = console;
            _config = config;
        }

        protected void OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
        }


        /// <summary>
        /// Call API with method returning meaningful object for success and ErrorContent for failure.
        /// </summary>
        /// <exception cref="Exception">When the result of API call is ErrorContent.</exception>
        protected static T CallApi<T>(Func<object> method)
        {
            object res;
            try
            {
                res = method.Invoke();
            }
            catch (HttpOperationException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Run 'config set' and add your Translator key or select proper configuration set by calling 'config select <name>'.");
                }
                else if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorDetails = JObject.Parse(ex.Response.Content);
                    throw new Exception("Error: " + (string)errorDetails["message"]);
                }
                else if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Invalid ID: target entity not found.");
                }
                throw;
            }

            return (T)res;
        }


        /// <summary>
        /// Call API with method.
        /// </summary>
        /// <exception cref="Exception">When the result of API call is ErrorContent.</exception>
        protected static void CallApi(Action method)
        {
            try
            {
                method.Invoke();
            }
            catch (HttpOperationException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Run 'config set' and add your Translator key or select proper configuration set by calling 'config select <name>'.");
                }
                else if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorDetails = JObject.Parse(ex.Response.Content);
                    throw new Exception("Error: " + (string)errorDetails["message"]);
                }
                else if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Invalid ID: target entity not found.");
                }
                throw;
            }
        }

        protected static int CreateAndWait<T>(Action operation, T id, bool wait, Func<T, bool> probe)
        {
            CallApi(operation);

            if (wait)
                return WaitForProcessing(id, probe);
            return 0;
         }

        protected static int WaitForProcessing<T>(T id, Func<T, bool> probe)
        {
            _console.Write("Processing [.");
            var done = false;
            while (!done)
            {
                _console.Write(".");
                Thread.Sleep(1000);
                done = probe.Invoke(id);
            }
            _console.WriteLine(".] Done");
            return 0;
        }
    }
}
