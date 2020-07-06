using CustomTranslatorCLI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomTranslatorCLI
{
    public class Config : IConfig
    {
        public static readonly string CONFIG_FILENAME = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "/.translator/config.json");

        public string Name { get; set; }
        public string TranslatorKey { get; set; }
        //public string TranslatorRegion { get; set; }
        public bool Selected { get; set; }

        public Config() { }

        //public Config(string defaultName = "", string defaultKey = "", string defaultRegion = "", bool defaultSelected = false)
        public Config(string defaultName = "", string defaultKey = "", bool defaultSelected = false)
        {
            Name = defaultName;
            TranslatorKey = defaultKey;
            //TranslatorRegion = defaultRegion;
        }
    }
}
