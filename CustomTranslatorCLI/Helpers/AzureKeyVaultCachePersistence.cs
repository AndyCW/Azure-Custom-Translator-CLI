using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace CustomTranslatorCLI.Helpers
{
    public class AzureKeyVaultCachePersistence : ICachePersistence
    {
        private IConfiguration appConfiguration;

        public AzureKeyVaultCachePersistence(IConfiguration config)
        {
            appConfiguration = config;
        }

        public void EnableSerialization(ITokenCache tokenCache)
        {
            tokenCache.SetBeforeAccess(BeforeAccessNotification);
            tokenCache.SetAfterAccess(AfterAccessNotification);
        }

        public void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            // Create a new secret client using the default credential from Azure.Identity using environment variables previously set,
            // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
            var clientId = appConfiguration["AZURE_CLIENT_ID"];
            var clientSecret = appConfiguration["AZURE_CLIENT_SECRET"];
            var tenantId = appConfiguration["AZURE_TENANT_ID"];
            var keyVaultUrl = appConfiguration["TRANSLATOR_VAULT_URI"];
            if (string.IsNullOrWhiteSpace(clientId))
            {
                Console.WriteLine("Please supply a AZURE_CLIENT_ID. See Readme.txt.");
                return;
            }
            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                Console.WriteLine("Please supply a AZURE_CLIENT_SECRET. See Readme.txt.");
                return;
            }
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                Console.WriteLine("Please supply a AZURE_TENANT_ID. See Readme.txt.");
                return;
            }
            if (string.IsNullOrWhiteSpace(keyVaultUrl))
            {
                Console.WriteLine("Please supply a TRANSLATOR_VAULT_URI. See Readme.txt.");
                return;
            }

            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());

            // Get a secret using the secret client.
            KeyVaultSecret secret = null;
            try
            {
                secret = client.GetSecret("token-cache");
            }
            catch (RequestFailedException ex)
            {
                if (ex.Status != (int)HttpStatusCode.NotFound)
                {
                    throw;
                }
            }

            args.TokenCache.DeserializeMsalV3(secret != null
                    ? Encoding.UTF8.GetBytes(secret.Value)
                    : null);

        }

        public void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                var client = new SecretClient(vaultUri: new Uri(appConfiguration["TRANSLATOR_VAULT_URI"]), credential: new DefaultAzureCredential());
                // reflect changes in the persistent store
                var secretValue = Encoding.UTF8.GetString(args.TokenCache.SerializeMsalV3());
                var resp = client.SetSecret("token-cache", secretValue);
                if (resp.GetRawResponse().Status != (int)HttpStatusCode.OK)
                {
                    Console.WriteLine("Failed to update token cache");
                }
            }
        }
    }
}