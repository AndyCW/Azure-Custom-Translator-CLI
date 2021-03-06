// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace CustomTranslator.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// User Information supplied for Tracking
    /// </summary>
    public partial class UserInfo
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseUserInfo class.
        /// </summary>
        public UserInfo()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsResponseUserInfo class.
        /// </summary>
        /// <param name="id">Gets or Sets the User Id</param>
        /// <param name="userName">Gets or Sets the User Name</param>
        public UserInfo(System.Guid id, string userName)
        {
            Id = id;
            UserName = userName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or Sets the User Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public System.Guid Id { get; set; }

        /// <summary>
        /// Gets or Sets the User Name
        /// </summary>
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (UserName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "UserName");
            }
        }
    }
}
