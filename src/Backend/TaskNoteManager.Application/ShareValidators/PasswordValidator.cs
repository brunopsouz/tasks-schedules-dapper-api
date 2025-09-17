using FluentValidation;
using FluentValidation.Validators;

namespace TaskNoteManager.Application.ShareValidators
{
    /// <summary>
    /// Validates a password based on predefined rules, such as empty password and minimum length.
    /// </summary>
    /// <remarks>This validator checks that the password is not null, empty, or whitespace, and that it meets
    /// a minimum length requirement. If the password is invalid, an error message is appended to the validation
    /// context.</remarks>
    /// <typeparam name="T">The type of the object being validated. Typically, this is the type of the model containing the password
    /// property.</typeparam>
    public class PasswordValidator<TRequest> : PropertyValidator<TRequest, string>
    {
        /// <summary>
        /// Gets the name of the validator.
        /// </summary>
        public override string Name => "PasswordValidator";

        /// <summary>
        /// Check if password is valid. 
        /// </summary>
        /// <param name="context">Validation context used to format error messages.</param>
        /// <param name="password">The password string to validate.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        public override bool IsValid(ValidationContext<TRequest> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", "Password cannot be empty.");
                return false;
            }

            if (password.Length < 6)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", "Password must have more than 6 characters");
            }

            return true;
        }

        /// <summary>
        /// Returns the default message template used for validation errors.
        /// The template includes a placeholder for a formatted error message.
        /// </summary>
        /// <param name="errorCode">The error code associated with the validation failure.</param>
        /// <returns>A string containing the default error message template.</returns>
        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
    }
}
