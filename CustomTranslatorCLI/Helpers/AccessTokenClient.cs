//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using RestSharp;
using Microsoft.Identity.Client;
using System.Linq;
using Microsoft.Extensions.Configuration;

#endregion

namespace CustomTranslatorCLI.Helpers
{
    public class AccessTokenClient
    {
        private const int MillisecondsTimeout = 100;

        #region Static Fields

        private IPublicClientApplication app;
        private IConfiguration appConfiguration;
        private string clientId;
        private string workspaceId;
        private string authorityUri;
        private string apiEndpoint;
        private List<string> scopes;

        /// <summary>
        /// Application(client) ID value - Create an app at the app registration portal
        /// https://ms.portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade
        /// </summary>
        public static string ClientId { get; set; }
        public static string WorkspaceId { get; set; }
        public static string TenantId { get; set; }
        public static List<string> Scopes { get; set; } = new List<string>() { "email" };

        /// <summary>
        /// End point address for V1 of Swagger UI API and Oauth V2 endpoint
        /// </summary>
        public static string EndPointAddressV1Prod { get; set; } = "https://custom-api.cognitive.microsofttranslator.com";
        public static string EndPointOauthV2 { get; set; }

        public enum ContentType { plain, HTML };
        #endregion

        #region Private Methods and Operators

        /// <summary>
        /// Loads credentials from settings file.
        /// Doesn't need to be public, because it is called during GetToken();
        /// </summary>
        private void LoadCredentials()
        {
            var x = appConfiguration.GetSection("AppRegistration");
            var xx = x.GetChildren();
            ClientId = appConfiguration.GetSection("AppRegistration")["clientId"];
            ClientId = "bff24280-701a-40d8-82d2-87822f30828d";
            if (string.IsNullOrWhiteSpace(ClientId))
            {
                Console.WriteLine("Please supply a clientId. See Readme.txt.");
                return;
            }

            TenantId = appConfiguration.GetSection("AppRegistration")["tenantId"];
            TenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47";
            if (string.IsNullOrWhiteSpace(TenantId))
            {
                Console.WriteLine("Please supply a tenantId.");
                return;
            }
            EndPointOauthV2 = $"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0";
        }

        #endregion

        #region Constructor

        public AccessTokenClient(IConfiguration configuration)
        {
            appConfiguration = configuration;
        }

        #endregion

        #region Public Methods and Operators

        public string GetToken()
        {
            LoadCredentials();
            clientId = AccessTokenClient.ClientId;
            workspaceId = AccessTokenClient.WorkspaceId;
            authorityUri = AccessTokenClient.EndPointOauthV2;
            apiEndpoint = AccessTokenClient.EndPointAddressV1Prod;
            scopes = AccessTokenClient.Scopes;

            app = PublicClientApplicationBuilder.Create(ClientId)
                    .WithRedirectUri("http://localhost")  
                    .Build();
            CachePersistence.EnableSerialization(app.UserTokenCache);

            string idToken = null;
            try
            {
                idToken = AcquireTokenSilent();
            }
            catch
            {
                idToken = AcquireTokenWithSignIn();
            }

            // Console.WriteLine("Id token Acquired Successfully. Use http://jwt.ms/ to inspect the token.");
            // Console.WriteLine("Token:");
            // Console.WriteLine(idToken);
            // Console.WriteLine();

            return idToken;
        }


        /// <summary>
        /// The silent sign-in. Relies on token cache.
        /// </summary>
        /// <returns></returns>
        public string AcquireTokenSilent()
        {
            var accounts = app.GetAccountsAsync().Result;
            var result = app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync().Result;
            return result.IdToken;
        }

        /// <summary>
        /// The INTERACTIVE sign in action. It redirects to AAD to sign the user in and get back the token of the user. 
        /// </summary>
        /// <returns></returns>        
        public string AcquireTokenWithSignIn()
        {
            var result = app.AcquireTokenInteractive(scopes).ExecuteAsync().Result;

            return result.IdToken;
        }

        #endregion
    }

}
