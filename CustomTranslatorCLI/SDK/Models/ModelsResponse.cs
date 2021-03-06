// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A container for models results.
    /// </summary>
    public partial class ModelsResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorModelsResponse class.
        /// </summary>
        public ModelsResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorModelsResponse class.
        /// </summary>
        /// <param name="modelsProperty">Gets or sets training models.</param>
        /// <param name="pageIndex">Gets or sets the page index.</param>
        /// <param name="totalPageCount">Gets or sets the total number of
        /// pages.</param>
        public ModelsResponse(IList<ModelInfo> modelsProperty, int pageIndex, int totalPageCount)
        {
            Models = modelsProperty;
            PageIndex = pageIndex;
            TotalPageCount = totalPageCount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets training models.
        /// </summary>
        [JsonProperty(PropertyName = "models")]
        public IList<ModelInfo> Models { get; set; }

        /// <summary>
        /// Gets or sets the page index.
        /// </summary>
        [JsonProperty(PropertyName = "pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        [JsonProperty(PropertyName = "totalPageCount")]
        public int TotalPageCount { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Models == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ModelsProperty");
            }
            if (Models != null)
            {
                foreach (var element in Models)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}
