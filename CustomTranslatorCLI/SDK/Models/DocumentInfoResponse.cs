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

    public partial class DocumentInfoResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorDocumentInfoResponse
        /// class.
        /// </summary>
        public DocumentInfoResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorDocumentInfoResponse
        /// class.
        /// </summary>
        /// <param name="documents">Gets or sets the documents.</param>
        /// <param name="pageIndex">Gets or sets the page index.</param>
        /// <param name="totalPageCount">Gets or sets the total number of
        /// pages.</param>
        public DocumentInfoResponse(IList<DocumentInfo> documents, int pageIndex, int totalPageCount)
        {
            Documents = documents;
            PageIndex = pageIndex;
            TotalPageCount = totalPageCount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public IList<DocumentInfo> Documents { get; set; }

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
            if (Documents == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Documents");
            }
            if (Documents != null)
            {
                foreach (var element in Documents)
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