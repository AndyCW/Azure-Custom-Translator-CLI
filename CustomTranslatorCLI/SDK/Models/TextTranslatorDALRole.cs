// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class TextTranslatorDALRole
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALRole class.
        /// </summary>
        public TextTranslatorDALRole()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALRole class.
        /// </summary>
        public TextTranslatorDALRole(int? id = default(int?), string roleName = default(string), IList<TextTranslatorDALApplicationSharing> applicationSharings = default(IList<TextTranslatorDALApplicationSharing>))
        {
            Id = id;
            RoleName = roleName;
            ApplicationSharings = applicationSharings;
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
        [JsonProperty(PropertyName = "roleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationSharings")]
        public IList<TextTranslatorDALApplicationSharing> ApplicationSharings { get; set; }

    }
}
