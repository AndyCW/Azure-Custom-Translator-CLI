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
    /// A container for workspace users results
    /// </summary>
    public partial class TextTranslatorModelsResponseTextTranslatorWorkspaceUsersResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorWorkspaceUsersResponse
        /// class.
        /// </summary>
        public TextTranslatorModelsResponseTextTranslatorWorkspaceUsersResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseTextTranslatorWorkspaceUsersResponse
        /// class.
        /// </summary>
        /// <param name="users">Gets or sets information for users with access
        /// to this workspace.</param>
        public TextTranslatorModelsResponseTextTranslatorWorkspaceUsersResponse(IList<TextTranslatorModelsSharingInfo> users)
        {
            Users = users;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets information for users with access to this workspace.
        /// </summary>
        [JsonProperty(PropertyName = "users")]
        public IList<TextTranslatorModelsSharingInfo> Users { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Users == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Users");
            }
            if (Users != null)
            {
                foreach (var element in Users)
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
