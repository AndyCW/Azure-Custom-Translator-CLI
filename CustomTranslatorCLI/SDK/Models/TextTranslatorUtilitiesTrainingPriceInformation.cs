// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TextTranslatorUtilitiesTrainingPriceInformation
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorUtilitiesTrainingPriceInformation class.
        /// </summary>
        public TextTranslatorUtilitiesTrainingPriceInformation()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorUtilitiesTrainingPriceInformation class.
        /// </summary>
        public TextTranslatorUtilitiesTrainingPriceInformation(bool? isPaidSubscription = default(bool?), int? maximumCharacterCount = default(int?), double? pricePerMillionCharacters = default(double?))
        {
            IsPaidSubscription = isPaidSubscription;
            MaximumCharacterCount = maximumCharacterCount;
            PricePerMillionCharacters = pricePerMillionCharacters;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isPaidSubscription")]
        public bool? IsPaidSubscription { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "maximumCharacterCount")]
        public int? MaximumCharacterCount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pricePerMillionCharacters")]
        public double? PricePerMillionCharacters { get; set; }

    }
}
