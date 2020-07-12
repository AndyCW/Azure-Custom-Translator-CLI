// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class TextTranslatorDALLanguage
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALLanguage class.
        /// </summary>
        public TextTranslatorDALLanguage()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALLanguage class.
        /// </summary>
        public TextTranslatorDALLanguage(int? id = default(int?), string displayName = default(string), string languageCode = default(string), IList<TextTranslatorDALLanguagePair> languagePairSourceLanguage = default(IList<TextTranslatorDALLanguagePair>), IList<TextTranslatorDALLanguagePair> languagePairTargetLanguage = default(IList<TextTranslatorDALLanguagePair>), IList<TextTranslatorDALFile> files = default(IList<TextTranslatorDALFile>))
        {
            Id = id;
            DisplayName = displayName;
            LanguageCode = languageCode;
            LanguagePairSourceLanguage = languagePairSourceLanguage;
            LanguagePairTargetLanguage = languagePairTargetLanguage;
            Files = files;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "languageCode")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "languagePairSourceLanguage")]
        public IList<TextTranslatorDALLanguagePair> LanguagePairSourceLanguage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "languagePairTargetLanguage")]
        public IList<TextTranslatorDALLanguagePair> LanguagePairTargetLanguage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "files")]
        public IList<TextTranslatorDALFile> Files { get; set; }

    }
}
