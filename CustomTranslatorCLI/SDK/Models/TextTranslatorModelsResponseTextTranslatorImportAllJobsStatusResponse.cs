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
    /// API results container for TextTranslatorImportAllJobs response.
    /// </summary>
    public partial class TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusResponse
        /// class.
        /// </summary>
        public TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusResponse
        /// class.
        /// </summary>
        /// <param name="jobs">Gets or sets the list of upload jobs.</param>
        /// <param name="pageIndex">Gets or sets the page index.</param>
        /// <param name="totalPageCount">Gets or sets the total number of
        /// pages.</param>
        public TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusResponse(IList<TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusInfo> jobs, int pageIndex, int totalPageCount)
        {
            Jobs = jobs;
            PageIndex = pageIndex;
            TotalPageCount = totalPageCount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the list of upload jobs.
        /// </summary>
        [JsonProperty(PropertyName = "jobs")]
        public IList<TextTranslatorModelsResponseTextTranslatorImportAllJobsStatusInfo> Jobs { get; set; }

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
            if (Jobs == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Jobs");
            }
            if (Jobs != null)
            {
                foreach (var element in Jobs)
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
