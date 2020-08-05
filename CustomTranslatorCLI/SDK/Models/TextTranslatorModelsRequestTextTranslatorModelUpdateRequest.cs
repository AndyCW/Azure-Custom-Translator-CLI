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

    /// <summary>
    /// The input class for model update requests.
    /// </summary>
    public partial class TextTranslatorModelsRequestTextTranslatorModelUpdateRequest
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsRequestTextTranslatorModelUpdateRequest class.
        /// </summary>
        public TextTranslatorModelsRequestTextTranslatorModelUpdateRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsRequestTextTranslatorModelUpdateRequest class.
        /// </summary>
        /// <param name="name">Gets or sets Name</param>
        /// <param name="addDocuments">Adds documents to the model.</param>
        /// <param name="removeDocuments">Removes documents from the
        /// model.</param>
        public TextTranslatorModelsRequestTextTranslatorModelUpdateRequest(string name = default(string), IList<long?> addDocuments = default(IList<long?>), IList<long?> removeDocuments = default(IList<long?>))
        {
            Name = name;
            AddDocuments = addDocuments;
            RemoveDocuments = removeDocuments;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets adds documents to the model.
        /// </summary>
        [JsonProperty(PropertyName = "addDocuments")]
        public IList<long?> AddDocuments { get; set; }

        /// <summary>
        /// Gets or sets removes documents from the model.
        /// </summary>
        [JsonProperty(PropertyName = "removeDocuments")]
        public IList<long?> RemoveDocuments { get; set; }

    }
}
