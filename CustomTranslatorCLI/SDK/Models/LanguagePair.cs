// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// A language pair that is supported by the text translator for
    /// transalation.
    /// </summary>
    public partial class LanguagePair
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorModelsLanguagePair
        /// class.
        /// </summary>
        public LanguagePair()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorModelsLanguagePair
        /// class.
        /// </summary>
        /// <param name="sourceLanguage">The First Language, could be a source
        /// or target</param>
        /// <param name="targetLanguage">The Second Language, could be a source
        /// or target</param>
        /// <param name="id">The Id for the language pair.</param>
        public LanguagePair(TranslatorLanguage sourceLanguage, TranslatorLanguage targetLanguage, long? id = default(long?))
        {
            Id = id;
            SourceLanguage = sourceLanguage;
            TargetLanguage = targetLanguage;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the Id for the language pair.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the First Language, could be a source or target
        /// </summary>
        [JsonProperty(PropertyName = "sourceLanguage")]
        public TranslatorLanguage SourceLanguage { get; set; }

        /// <summary>
        /// Gets or sets the Second Language, could be a source or target
        /// </summary>
        [JsonProperty(PropertyName = "targetLanguage")]
        public TranslatorLanguage TargetLanguage { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (SourceLanguage == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "SourceLanguage");
            }
            if (TargetLanguage == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TargetLanguage");
            }
            if (SourceLanguage != null)
            {
                SourceLanguage.Validate();
            }
            if (TargetLanguage != null)
            {
                TargetLanguage.Validate();
            }
        }
    }
}
