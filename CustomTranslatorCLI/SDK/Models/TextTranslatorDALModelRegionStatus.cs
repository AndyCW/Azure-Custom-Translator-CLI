// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TextTranslatorDALModelRegionStatus
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALModelRegionStatus class.
        /// </summary>
        public TextTranslatorDALModelRegionStatus()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALModelRegionStatus class.
        /// </summary>
        public TextTranslatorDALModelRegionStatus(long? id = default(long?), long? modelId = default(long?), int? region = default(int?), bool? isDeployed = default(bool?), int? deploymentStatus = default(int?), TextTranslatorDALModel model = default(TextTranslatorDALModel))
        {
            Id = id;
            ModelId = modelId;
            Region = region;
            IsDeployed = isDeployed;
            DeploymentStatus = deploymentStatus;
            Model = model;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modelId")]
        public long? ModelId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "region")]
        public int? Region { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isDeployed")]
        public bool? IsDeployed { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "deploymentStatus")]
        public int? DeploymentStatus { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "model")]
        public TextTranslatorDALModel Model { get; set; }

    }
}
