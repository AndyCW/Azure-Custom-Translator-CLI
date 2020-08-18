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
using Microsoft.Identity.Client;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CustomTranslatorCLI.Interfaces;

#endregion

namespace CustomTranslatorCLI.Helpers
{
    public class AccessTokenClient : IAccessTokenClient
    {
        #region Static Fields

        private IPublicClientApplication app;
        private IConfiguration appConfiguration;
        private ICachePersistence cachePersistence;

        /// <summary>
        /// Application(client) ID value - Create an app at the app registration portal
        /// https://ms.portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade
        /// </summary>
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        public static string TenantId { get; set; }
        public static List<string> Scopes { get; set; } = new List<string>() { "email" };

        protected static string mBearerToken;

        #endregion

        #region Private Methods and Operators

        /// <summary>
        /// Loads credentials from settings file.
        /// Doesn't need to be public, because it is called during GetToken();
        /// </summary>
        private void LoadCredentials()
        {
            ClientId = appConfiguration["AZURE_CLIENT_ID"];
            ClientSecret = appConfiguration["AZURE_CLIENT_SECRET"];
            TenantId = appConfiguration["AZURE_TENANT_ID"];
            var keyVaultUrl = appConfiguration["TRANSLATOR_VAULT_URI"];

            if (string.IsNullOrWhiteSpace(ClientId))
            {
                Console.WriteLine("Please supply a AZURE_CLIENT_ID. See Readme.txt.");
                return;
            }
            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                Console.WriteLine("Please supply a AZURE_CLIENT_SECRET. See Readme.txt.");
                return;
            }
            if (string.IsNullOrWhiteSpace(TenantId))
            {
                Console.WriteLine("Please supply a AZURE_TENANT_ID. See Readme.txt.");
                return;
            }
        }

        #endregion

        #region Constructor

        public AccessTokenClient(IConfiguration configuration, ICachePersistence cachepersistence)
        {
            appConfiguration = configuration;
            cachePersistence = cachepersistence;
        }

        #endregion

        #region Public Methods and Operators

        public string GetToken()
        {
            if (!string.IsNullOrEmpty(mBearerToken))
            {
                return mBearerToken;
            }
            else
            {

                LoadCredentials();

                app = PublicClientApplicationBuilder.Create(ClientId)
                        .WithRedirectUri("http://localhost")
                        .Build();
                cachePersistence.EnableSerialization(app.UserTokenCache);

                string idToken;
                try
                {
                    idToken = AcquireTokenSilent();
                }
                catch
                {
                    idToken = AcquireTokenWithSignIn();
                }
                mBearerToken = "Bearer " + idToken;

                return mBearerToken;
            }
        }


        /// <summary>
        /// The silent sign-in. Relies on token cache.
        /// </summary>
        /// <returns></returns>
        private string AcquireTokenSilent()
        {
            var accounts = app.GetAccountsAsync().Result;
            var result = app.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync().Result;
            return result.IdToken;
        }

        /// <summary>
        /// The INTERACTIVE sign in action. It redirects to AAD to sign the user in and get back the token of the user. 
        /// </summary>
        /// <returns></returns>        
        private string AcquireTokenWithSignIn()
        {
            var result = app.AcquireTokenInteractive(Scopes).ExecuteAsync().Result;

            return result.IdToken;
        }

        #endregion
    }

}
