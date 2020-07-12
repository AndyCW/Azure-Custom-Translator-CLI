// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TextTranslatorDALProjectCredit
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALProjectCredit
        /// class.
        /// </summary>
        public TextTranslatorDALProjectCredit()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALProjectCredit
        /// class.
        /// </summary>
        public TextTranslatorDALProjectCredit(int? id = default(int?), byte[] type = default(byte[]), int? credits = default(int?), System.Guid? projectId = default(System.Guid?), TextTranslatorDALProject project = default(TextTranslatorDALProject))
        {
            Id = id;
            Type = type;
            Credits = credits;
            ProjectId = projectId;
            Project = project;
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
        [JsonProperty(PropertyName = "type")]
        public byte[] Type { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "credits")]
        public int? Credits { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "projectId")]
        public System.Guid? ProjectId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "project")]
        public TextTranslatorDALProject Project { get; set; }

    }
}
