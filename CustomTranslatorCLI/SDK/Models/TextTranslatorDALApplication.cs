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

    public partial class TextTranslatorDALApplication
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALApplication
        /// class.
        /// </summary>
        public TextTranslatorDALApplication()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorDALApplication
        /// class.
        /// </summary>
        public TextTranslatorDALApplication(int? id = default(int?), string applicationName = default(string), string applicationIdentifier = default(string), System.DateTime? createdDate = default(System.DateTime?), System.Guid? createdBy = default(System.Guid?), System.DateTime? modifiedDate = default(System.DateTime?), System.Guid? modifiedBy = default(System.Guid?), TextTranslatorDALApplicationUser createdByNavigation = default(TextTranslatorDALApplicationUser), IList<TextTranslatorDALApplicationSubscriptions> applicationSubscriptions = default(IList<TextTranslatorDALApplicationSubscriptions>), IList<TextTranslatorDALDocument> document = default(IList<TextTranslatorDALDocument>), IList<TextTranslatorDALFile> file = default(IList<TextTranslatorDALFile>), IList<TextTranslatorDALProject> project = default(IList<TextTranslatorDALProject>), IList<TextTranslatorDALUploadHistory> uploadHistory = default(IList<TextTranslatorDALUploadHistory>), IList<TextTranslatorDALApplicationSharing> applicationSharings = default(IList<TextTranslatorDALApplicationSharing>), IList<TextTranslatorDALApplicationCredit> applicationCredit = default(IList<TextTranslatorDALApplicationCredit>))
        {
            Id = id;
            ApplicationName = applicationName;
            ApplicationIdentifier = applicationIdentifier;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            CreatedByNavigation = createdByNavigation;
            ApplicationSubscriptions = applicationSubscriptions;
            Document = document;
            File = file;
            Project = project;
            UploadHistory = uploadHistory;
            ApplicationSharings = applicationSharings;
            ApplicationCredit = applicationCredit;
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
        [JsonProperty(PropertyName = "applicationName")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationIdentifier")]
        public string ApplicationIdentifier { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdDate")]
        public System.DateTime? CreatedDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdBy")]
        public System.Guid? CreatedBy { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedDate")]
        public System.DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedBy")]
        public System.Guid? ModifiedBy { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdByNavigation")]
        public TextTranslatorDALApplicationUser CreatedByNavigation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationSubscriptions")]
        public IList<TextTranslatorDALApplicationSubscriptions> ApplicationSubscriptions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "document")]
        public IList<TextTranslatorDALDocument> Document { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "file")]
        public IList<TextTranslatorDALFile> File { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "project")]
        public IList<TextTranslatorDALProject> Project { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "uploadHistory")]
        public IList<TextTranslatorDALUploadHistory> UploadHistory { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationSharings")]
        public IList<TextTranslatorDALApplicationSharing> ApplicationSharings { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationCredit")]
        public IList<TextTranslatorDALApplicationCredit> ApplicationCredit { get; set; }

    }
}
