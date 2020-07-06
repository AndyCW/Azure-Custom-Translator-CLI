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
    /// The input class for creating a new workspace.
    /// </summary>
    public partial class TextTranslatorModelsRequestTextTranslatorCreateWorkspaceRequest
    {
        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsRequestTextTranslatorCreateWorkspaceRequest
        /// class.
        /// </summary>
        public TextTranslatorModelsRequestTextTranslatorCreateWorkspaceRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// TextTranslatorModelsRequestTextTranslatorCreateWorkspaceRequest
        /// class.
        /// </summary>
        /// <param name="name">Gets or sets the name of the workspace</param>
        /// <param name="subscription">Gets or sets the subscription
        /// information</param>
        public TextTranslatorModelsRequestTextTranslatorCreateWorkspaceRequest(string name, TextTranslatorModelsRequestTextTranslatorSubscriptionRequest subscription)
        {
            Name = name;
            Subscription = subscription;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the name of the workspace
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the subscription information
        /// </summary>
        [JsonProperty(PropertyName = "subscription")]
        public TextTranslatorModelsRequestTextTranslatorSubscriptionRequest Subscription { get; set; }

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
            if (Subscription == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Subscription");
            }
            if (Subscription != null)
            {
                Subscription.Validate();
            }
        }
    }
}
