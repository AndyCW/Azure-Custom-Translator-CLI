// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TextTranslatorDALUnsupportedDeployedHubModel
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALUnsupportedDeployedHubModel class.
        /// </summary>
        public TextTranslatorDALUnsupportedDeployedHubModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALUnsupportedDeployedHubModel class.
        /// </summary>
        public TextTranslatorDALUnsupportedDeployedHubModel(long? hubProjectId = default(long?), System.Guid? ctProjectId = default(System.Guid?), string hubCategoryId = default(string), long? modelId = default(long?), bool? isDeployed = default(bool?), bool? isDeleted = default(bool?), TextTranslatorDALModel model = default(TextTranslatorDALModel))
        {
            HubProjectId = hubProjectId;
            CtProjectId = ctProjectId;
            HubCategoryId = hubCategoryId;
            ModelId = modelId;
            IsDeployed = isDeployed;
            IsDeleted = isDeleted;
            Model = model;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hubProjectId")]
        public long? HubProjectId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ctProjectId")]
        public System.Guid? CtProjectId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hubCategoryId")]
        public string HubCategoryId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modelId")]
        public long? ModelId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isDeployed")]
        public bool? IsDeployed { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isDeleted")]
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "model")]
        public TextTranslatorDALModel Model { get; set; }

    }
}
