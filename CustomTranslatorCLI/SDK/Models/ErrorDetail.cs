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
    /// ErrorDetail
    /// </summary>
    /// <remarks>
    /// An array of implementations of this interface can be used as details
    /// for an error.
    /// </remarks>
    public partial class ErrorDetail
    {
        /// <summary>
        /// Initializes a new instance of the ErrorDetail class.
        /// </summary>
        public ErrorDetail()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ErrorDetail class.
        /// </summary>
        /// <param name="code">A service-defined error code that should be
        /// human-readable.
        /// This code serves as a more specific indicator of the error than
        /// the HTTP error code specified in the response</param>
        /// <param name="message">A human-readable representation of the error.
        /// It is intended as
        /// an aid to developers and is not suitable for exposure to end
        /// users</param>
        /// <param name="target">The target of the particular error (e.g., the
        /// name of the property in error)</param>
        public ErrorDetail(string code, string message, string target = default(string))
        {
            Code = code;
            Message = message;
            Target = target;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets a service-defined error code that should be
        /// human-readable.
        /// This code serves as a more specific indicator of the error than
        /// the HTTP error code specified in the response
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a human-readable representation of the error. It is
        /// intended as
        /// an aid to developers and is not suitable for exposure to end users
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the target of the particular error (e.g., the name of
        /// the property in error)
        /// </summary>
        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Code == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Code");
            }
            if (Message == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Message");
            }
        }
    }
}