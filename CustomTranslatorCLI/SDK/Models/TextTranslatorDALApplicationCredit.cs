// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TextTranslatorDALApplicationCredit
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALApplicationCredit class.
        /// </summary>
        public TextTranslatorDALApplicationCredit()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorDALApplicationCredit class.
        /// </summary>
        public TextTranslatorDALApplicationCredit(int? id = default(int?), byte[] type = default(byte[]), int? credits = default(int?), int? applicationId = default(int?), TextTranslatorDALApplication application = default(TextTranslatorDALApplication))
        {
            Id = id;
            Type = type;
            Credits = credits;
            ApplicationId = applicationId;
            Application = application;
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
        [JsonProperty(PropertyName = "applicationId")]
        public int? ApplicationId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "application")]
        public TextTranslatorDALApplication Application { get; set; }

    }
}