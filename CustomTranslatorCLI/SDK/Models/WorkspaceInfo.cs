// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Metadata about an individual workspace
    /// </summary>
    //[JsonArray(true)]
    public partial class WorkspaceInfo
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorApiModelsTextTranslatorWorkspaceInfo class.
        /// </summary>
        public WorkspaceInfo()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorApiModelsTextTranslatorWorkspaceInfo class.
        /// </summary>
        /// <param name="id">Gets or sets the workspace id.</param>
        /// <param name="role">Gets or sets the role this user has in the
        /// workspace.</param>
        /// <param name="name">Gets or sets the name of the workspace.</param>
        /// <param name="isCreator">Gets or sets if this user is the creator of
        /// the workspace.</param>
        /// <param name="isDefaultWorkspace">Gets or sets if this user has this
        /// workspace set as their default workspace.</param>
        /// <param name="createdBy">Gets or sets who created the
        /// workspace.</param>
        /// <param name="createdDate">Gets or sets when the workspace was
        /// created.</param>
        /// <param name="sharing">Gets or sets the information about who this
        /// workspace is shared with.</param>
        /// <param name="hasMigrations">Gets or sets if the current workspace
        /// has any migrations.</param>
        public WorkspaceInfo(string id = default(string), RoleInfo role = default(RoleInfo), string name = default(string), bool? isCreator = default(bool?), bool? isDefaultWorkspace = default(bool?), UserInfo createdBy = default(UserInfo), System.DateTime? createdDate = default(System.DateTime?), IList<SharingInfo> sharing = default(IList<SharingInfo>), bool? hasMigrations = default(bool?))
        {
            Id = id;
            Role = role;
            Name = name;
            IsCreator = isCreator;
            IsDefaultWorkspace = isDefaultWorkspace;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            Sharing = sharing;
            HasMigrations = hasMigrations;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the workspace id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the role this user has in the workspace.
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public RoleInfo Role { get; set; }

        /// <summary>
        /// Gets or sets the name of the workspace.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets if this user is the creator of the workspace.
        /// </summary>
        [JsonProperty(PropertyName = "isCreator")]
        public bool? IsCreator { get; set; }

        /// <summary>
        /// Gets or sets if this user has this workspace set as their default
        /// workspace.
        /// </summary>
        [JsonProperty(PropertyName = "isDefaultWorkspace")]
        public bool? IsDefaultWorkspace { get; set; }

        /// <summary>
        /// Gets or sets who created the workspace.
        /// </summary>
        [JsonProperty(PropertyName = "createdBy")]
        public UserInfo CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets when the workspace was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdDate")]
        public System.DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the information about who this workspace is shared
        /// with.
        /// </summary>
        [JsonProperty(PropertyName = "sharing")]
        public IList<SharingInfo> Sharing { get; set; }

        /// <summary>
        /// Gets or sets if the current workspace has any migrations.
        /// </summary>
        [JsonProperty(PropertyName = "hasMigrations")]
        public bool? HasMigrations { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Role != null)
            {
                Role.Validate();
            }
            if (CreatedBy != null)
            {
                CreatedBy.Validate();
            }
            if (Sharing != null)
            {
                foreach (var element in Sharing)
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
