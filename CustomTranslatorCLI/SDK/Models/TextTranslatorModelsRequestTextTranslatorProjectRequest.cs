// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The input class for model requests.
    /// </summary>
    public partial class TextTranslatorModelsRequestTextTranslatorProjectRequest
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsRequestTextTranslatorProjectRequest class.
        /// </summary>
        public TextTranslatorModelsRequestTextTranslatorProjectRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsRequestTextTranslatorProjectRequest class.
        /// </summary>
        /// <param name="languagePairId">The Id for the language pair.</param>
        /// <param name="categoryId">Gets or sets Category</param>
        /// <param name="name">Gets or sets Name</param>
        /// <param name="categoryDescriptor">Gets or sets
        /// CategoryDescriptor</param>
        /// <param name="description">Gets or sets Description</param>
        /// <param name="label">Gets or sets the project label.</param>
        public TextTranslatorModelsRequestTextTranslatorProjectRequest(int languagePairId, int categoryId, string name = default(string), string categoryDescriptor = default(string), string description = default(string), string label = default(string))
        {
            LanguagePairId = languagePairId;
            CategoryId = categoryId;
            Name = name;
            CategoryDescriptor = categoryDescriptor;
            Description = description;
            Label = label;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the Id for the language pair.
        /// </summary>
        [JsonProperty(PropertyName = "languagePairId")]
        public int LanguagePairId { get; set; }

        /// <summary>
        /// Gets or sets Category
        /// </summary>
        [JsonProperty(PropertyName = "categoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets CategoryDescriptor
        /// </summary>
        [JsonProperty(PropertyName = "categoryDescriptor")]
        public string CategoryDescriptor { get; set; }

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the project label.
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

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
