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
    /// Class containing detailed information about document.
    /// </summary>
    public partial class TextTranslatorModelsTextTranslatorDocumentInfo
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsTextTranslatorDocumentInfo class.
        /// </summary>
        public TextTranslatorModelsTextTranslatorDocumentInfo()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsTextTranslatorDocumentInfo class.
        /// </summary>
        /// <param name="name">Gets or sets Name</param>
        /// <param name="isParallel">Gets or sets IsParallel - paired/unpaired
        /// with source document</param>
        /// <param name="createdBy">Gets the user who created this
        /// document.</param>
        /// <param name="modifiedBy">Gets the user who last modified this
        /// document.</param>
        /// <param name="id">Gets or sets Id</param>
        /// <param name="documentType">Gets or sets the DocumentType. Possible
        /// values include: 'none', 'training', 'testing', 'tuning',
        /// 'phraseDictionary', 'sentenceDictionary'</param>
        /// <param name="extractedSentenceCount">Gets or sets extracted
        /// sentence count.</param>
        /// <param name="characterCount">Gets or sets character count.</param>
        /// <param name="files">Gets or sets the files associated with the
        /// document. Each document may have many files, one for each language.
        /// If document has one file, that may mean that only one language data
        /// was uploaded for it, and others still need to be provided.</param>
        /// <param name="languages">Gets or sets the file languages associated
        /// with the document.</param>
        /// <param name="createdDate">Gets or sets CreatedDate</param>
        /// <param name="isAvailable">A document is available when it is NOT
        /// 1. A monolingual document
        /// 2. A dictionary
        /// 3. Has a language pair that is not supported</param>
        /// <param name="usedByPrioritizedModel">Gets or sets if this model is
        /// used by the specified prioritized model</param>
        public TextTranslatorModelsTextTranslatorDocumentInfo(string name, bool isParallel, UserInfo createdBy, UserInfo modifiedBy, long id, string documentType, long extractedSentenceCount, long characterCount, IList<TextTranslatorModelsTextTranslatorFileInfo> files = default(IList<TextTranslatorModelsTextTranslatorFileInfo>), IList<TextTranslatorModelsTextTranslatorLanguage> languages = default(IList<TextTranslatorModelsTextTranslatorLanguage>), System.DateTime? createdDate = default(System.DateTime?), bool? isAvailable = default(bool?), bool? usedByPrioritizedModel = default(bool?))
        {
            Name = name;
            IsParallel = isParallel;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
            Files = files;
            Languages = languages;
            CreatedDate = createdDate;
            IsAvailable = isAvailable;
            Id = id;
            DocumentType = documentType;
            ExtractedSentenceCount = extractedSentenceCount;
            CharacterCount = characterCount;
            UsedByPrioritizedModel = usedByPrioritizedModel;
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
        /// Gets or sets IsParallel - paired/unpaired with source document
        /// </summary>
        [JsonProperty(PropertyName = "isParallel")]
        public bool IsParallel { get; set; }

        /// <summary>
        /// Gets the user who created this document.
        /// </summary>
        [JsonProperty(PropertyName = "createdBy")]
        public UserInfo CreatedBy { get; set; }

        /// <summary>
        /// Gets the user who last modified this document.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedBy")]
        public UserInfo ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the files associated with the document. Each document
        /// may have many files, one for each language.
        /// If document has one file, that may mean that only one language data
        /// was uploaded for it, and others still need to be provided.
        /// </summary>
        [JsonProperty(PropertyName = "files")]
        public IList<TextTranslatorModelsTextTranslatorFileInfo> Files { get; set; }

        /// <summary>
        /// Gets or sets the file languages associated with the document.
        /// </summary>
        [JsonProperty(PropertyName = "languages")]
        public IList<TextTranslatorModelsTextTranslatorLanguage> Languages { get; set; }

        /// <summary>
        /// Gets or sets CreatedDate
        /// </summary>
        [JsonProperty(PropertyName = "createdDate")]
        public System.DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a document is available when it is NOT
        /// 1. A monolingual document
        /// 2. A dictionary
        /// 3. Has a language pair that is not supported
        /// </summary>
        [JsonProperty(PropertyName = "isAvailable")]
        public bool? IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the DocumentType. Possible values include: 'none',
        /// 'training', 'testing', 'tuning', 'phraseDictionary',
        /// 'sentenceDictionary'
        /// </summary>
        [JsonProperty(PropertyName = "documentType")]
        public string DocumentType { get; set; }

        /// <summary>
        /// Gets or sets extracted sentence count.
        /// </summary>
        [JsonProperty(PropertyName = "extractedSentenceCount")]
        public long ExtractedSentenceCount { get; set; }

        /// <summary>
        /// Gets or sets character count.
        /// </summary>
        [JsonProperty(PropertyName = "characterCount")]
        public long CharacterCount { get; set; }

        /// <summary>
        /// Gets or sets if this model is used by the specified prioritized
        /// model
        /// </summary>
        [JsonProperty(PropertyName = "usedByPrioritizedModel")]
        public bool? UsedByPrioritizedModel { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
            if (CreatedBy == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "CreatedBy");
            }
            if (ModifiedBy == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ModifiedBy");
            }
            if (DocumentType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "DocumentType");
            }
            if (CreatedBy != null)
            {
                CreatedBy.Validate();
            }
            if (ModifiedBy != null)
            {
                ModifiedBy.Validate();
            }
            if (Files != null)
            {
                foreach (var element in Files)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (Languages != null)
            {
                foreach (var element1 in Languages)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
        }
    }
}
