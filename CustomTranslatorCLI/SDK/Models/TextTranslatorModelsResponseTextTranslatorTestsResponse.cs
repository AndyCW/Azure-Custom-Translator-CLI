// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Result container for tests.
    /// </summary>
    public partial class TextTranslatorModelsResponseTextTranslatorTestsResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorTestsResponse class.
        /// </summary>
        public TextTranslatorModelsResponseTextTranslatorTestsResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorTestsResponse class.
        /// </summary>
        /// <param name="tests">Gets or sets Tests</param>
        /// <param name="pageIndex">Gets or sets the page index.</param>
        /// <param name="totalPageCount">Gets or sets the total number of
        /// pages.</param>
        public TextTranslatorModelsResponseTextTranslatorTestsResponse(IList<TextTranslatorModelsTextTranslatorTestInfo> tests, int pageIndex, int totalPageCount)
        {
            Tests = tests;
            PageIndex = pageIndex;
            TotalPageCount = totalPageCount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets Tests
        /// </summary>
        [JsonProperty(PropertyName = "tests")]
        public IList<TextTranslatorModelsTextTranslatorTestInfo> Tests { get; set; }

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
            if (Tests == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Tests");
            }
            if (Tests != null)
            {
                foreach (var element in Tests)
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
