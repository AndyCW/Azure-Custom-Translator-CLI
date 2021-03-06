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

    public partial class TextTranslatorDALBillingRegions
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALBillingRegions
        /// class.
        /// </summary>
        public TextTranslatorDALBillingRegions()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALBillingRegions
        /// class.
        /// </summary>
        public TextTranslatorDALBillingRegions(string billingRegionCode = default(string), string billingRegionName = default(string), string billingEndpoint = default(string), string meteringKeySecretName = default(string), IList<TextTranslatorDALApplicationSubscriptions> applicationSubscriptionsBillingRegion = default(IList<TextTranslatorDALApplicationSubscriptions>), IList<TextTranslatorDALApplicationSubscriptions> applicationSubscriptionsDataRegion = default(IList<TextTranslatorDALApplicationSubscriptions>))
        {
            BillingRegionCode = billingRegionCode;
            BillingRegionName = billingRegionName;
            BillingEndpoint = billingEndpoint;
            MeteringKeySecretName = meteringKeySecretName;
            ApplicationSubscriptionsBillingRegion = applicationSubscriptionsBillingRegion;
            ApplicationSubscriptionsDataRegion = applicationSubscriptionsDataRegion;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingRegionCode")]
        public string BillingRegionCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingRegionName")]
        public string BillingRegionName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingEndpoint")]
        public string BillingEndpoint { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "meteringKeySecretName")]
        public string MeteringKeySecretName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationSubscriptionsBillingRegion")]
        public IList<TextTranslatorDALApplicationSubscriptions> ApplicationSubscriptionsBillingRegion { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationSubscriptionsDataRegion")]
        public IList<TextTranslatorDALApplicationSubscriptions> ApplicationSubscriptionsDataRegion { get; set; }

    }
}
