using CustomTranslatorCLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azure_Custom_Translator_CLI.Tests.Utils
{
    public class MockAccessTokenClient : IAccessTokenClient
    {
        public string GetToken()
        {
            return string.Empty;
        }
    }
}
