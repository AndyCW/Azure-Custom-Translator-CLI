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
    /// Information about a region a model is deployed or not deployed in
    /// </summary>
    public partial class ModelRegionStatus
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsTextTranslatorModelRegionStatus class.
        /// </summary>
        public ModelRegionStatus()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsTextTranslatorModelRegionStatus class.
        /// </summary>
        /// <param name="region">Gets or sets the RegionId</param>
        /// <param name="isDeployed">Gets or sets the id for this role.</param>
        public ModelRegionStatus(int region, bool isDeployed)
        {
            Region = region;
            IsDeployed = isDeployed;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the RegionId
        /// </summary>
        [JsonProperty(PropertyName = "region")]
        public int Region { get; set; }

        /// <summary>
        /// Gets or sets the id for this role.
        /// </summary>
        [JsonProperty(PropertyName = "isDeployed")]
        public bool IsDeployed { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
