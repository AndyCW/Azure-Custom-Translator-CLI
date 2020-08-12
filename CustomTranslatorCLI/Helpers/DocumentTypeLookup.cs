using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTranslatorCLI.Helpers
{
    public static class DocumentTypeLookup
    {
        public static Dictionary<string, string> Types = new Dictionary<string, string>()
        {
            {"training", "Training" },
            {"testing", "Testing" },
            {"tuning", "Tuning" },
            {"phrase", "Phrase Dictionary" },
            {"sentence", "Sentence Dictionary" }
        };
    }
}
