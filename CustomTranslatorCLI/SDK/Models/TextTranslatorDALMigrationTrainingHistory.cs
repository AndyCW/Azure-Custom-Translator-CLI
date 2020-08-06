// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TextTranslatorDALMigrationTrainingHistory
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALMigrationTrainingHistory class.
        /// </summary>
        public TextTranslatorDALMigrationTrainingHistory()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALMigrationTrainingHistory class.
        /// </summary>
        public TextTranslatorDALMigrationTrainingHistory(string migrationId = default(string), long? sourceID = default(long?), long? modelId = default(long?), int? status = default(int?), string sourceWorkspace = default(string), string targetWorkspace = default(string), double? hubBLEUScore = default(double?), string regions = default(string), TextTranslatorDALModel model = default(TextTranslatorDALModel))
        {
            MigrationId = migrationId;
            SourceID = sourceID;
            ModelId = modelId;
            Status = status;
            SourceWorkspace = sourceWorkspace;
            TargetWorkspace = targetWorkspace;
            HubBLEUScore = hubBLEUScore;
            Regions = regions;
            Model = model;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "migrationId")]
        public string MigrationId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sourceID")]
        public long? SourceID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modelId")]
        public long? ModelId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int? Status { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sourceWorkspace")]
        public string SourceWorkspace { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "targetWorkspace")]
        public string TargetWorkspace { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hubBLEUScore")]
        public double? HubBLEUScore { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regions")]
        public string Regions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "model")]
        public TextTranslatorDALModel Model { get; set; }

    }
}