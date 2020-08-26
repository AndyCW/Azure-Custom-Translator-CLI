// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Defines an application subscription.
    /// </summary>
    public partial class TextTranslatorModelsTextTranslatorSubscriptionResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsTextTranslatorSubscriptionResponse class.
        /// </summary>
        public TextTranslatorModelsTextTranslatorSubscriptionResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsTextTranslatorSubscriptionResponse class.
        /// </summary>
        /// <param name="id">Subscription id</param>
        /// <param name="name">Name of users Azure resource</param>
        /// <param name="kind">Kind of subscription
        /// Example: TextTranslation</param>
        /// <param name="sku">Subscription SKU type
        /// Example: S1</param>
        /// <param name="isCMKEnabled">If CMK is enabled on this
        /// subscription</param>
        /// <param name="region">Information about what region this
        /// subscription
        /// resides in</param>
        /// <param name="billing">Includes price per million characters, if
        /// subscription
        /// is paid or free</param>
        public TextTranslatorModelsTextTranslatorSubscriptionResponse(string id = default(string), string name = default(string), string kind = default(string), string sku = default(string), bool? isCMKEnabled = default(bool?), BillingRegions region = default(BillingRegions), TextTranslatorUtilitiesTrainingPriceInformation billing = default(TextTranslatorUtilitiesTrainingPriceInformation))
        {
            Id = id;
            Name = name;
            Kind = kind;
            Sku = sku;
            IsCMKEnabled = isCMKEnabled;
            Region = region;
            Billing = billing;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets subscription id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name of users Azure resource
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets kind of subscription
        /// Example: TextTranslation
        /// </summary>
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets subscription SKU type
        /// Example: S1
        /// </summary>
        [JsonProperty(PropertyName = "sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets if CMK is enabled on this subscription
        /// </summary>
        [JsonProperty(PropertyName = "isCMKEnabled")]
        public bool? IsCMKEnabled { get; set; }

        /// <summary>
        /// Gets or sets information about what region this subscription
        /// resides in
        /// </summary>
        [JsonProperty(PropertyName = "region")]
        public BillingRegions Region { get; set; }

        /// <summary>
        /// Gets or sets includes price per million characters, if subscription
        /// is paid or free
        /// </summary>
        [JsonProperty(PropertyName = "billing")]
        public TextTranslatorUtilitiesTrainingPriceInformation Billing { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Region != null)
            {
                Region.Validate();
            }
        }
    }
}
