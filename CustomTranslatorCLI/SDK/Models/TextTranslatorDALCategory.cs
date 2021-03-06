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

    public partial class TextTranslatorDALCategory
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALCategory class.
        /// </summary>
        public TextTranslatorDALCategory()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALCategory class.
        /// </summary>
        public TextTranslatorDALCategory(int? id = default(int?), string name = default(string), string code = default(string), IList<TextTranslatorDALProject> project = default(IList<TextTranslatorDALProject>))
        {
            Id = id;
            Name = name;
            Code = code;
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
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "project")]
        public IList<TextTranslatorDALProject> Project { get; set; }

    }
}
