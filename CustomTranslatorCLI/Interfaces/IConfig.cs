using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTranslatorCLI.Interfaces
{
    public interface IConfig
    {
        string Name { get; set; }
        string TranslatorKey { get; set; }
        string TranslatorRegion { get; set; }
        bool Selected { get; set; }
    }
}
