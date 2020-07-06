// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Information about a user that has permissions to a workspace.
    /// </summary>
    public partial class TextTranslatorModelsSharingInfo
    {
        /// <summary>
        /// Initializes a new instance of the TextTranslatorModelsSharingInfo
        /// class.
        /// </summary>
        public TextTranslatorModelsSharingInfo()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextTranslatorModelsSharingInfo
        /// class.
        /// </summary>
        /// <param name="id">Gets or sets the id for the user this workspace
        /// has been
        /// shared with.</param>
        /// <param name="emailAddress">Gets or sets the email address for the
        /// user this workspace has been
        /// shared with.</param>
        /// <param name="role">Gets or sets the role this user has in the
        /// workspace.</param>
        public TextTranslatorModelsSharingInfo(long id, string emailAddress, TextTranslatorModelsRoleInfo role)
        {
            Id = id;
            EmailAddress = emailAddress;
            Role = role;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the id for the user this workspace has been
        /// shared with.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the email address for the user this workspace has been
        /// shared with.
        /// </summary>
        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the role this user has in the workspace.
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public TextTranslatorModelsRoleInfo Role { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (EmailAddress == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "EmailAddress");
            }
            if (Role == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Role");
            }
            if (Role != null)
            {
                Role.Validate();
            }
        }
    }
}
