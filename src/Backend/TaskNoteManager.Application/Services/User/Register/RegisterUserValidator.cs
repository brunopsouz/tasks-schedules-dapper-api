using FluentValidation;
using TaskNoteManager.Application.ShareValidators;
using TaskNoteManager.Communication.Requests;

namespace TaskNoteManager.Application.Services.User.Register
{
    /// <summary>
    /// Defines validation rules for user registration requests.
    /// Ensures required fields are provided and that enum values are valid.
    /// Includes custom password validation (PasswordValidator<TRequest>) to check if Password is valid.
    /// </summary>
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(user => user.Email).NotEmpty().WithMessage("E-mail is required.");
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUser>());
            RuleFor(user => user.Position).IsInEnum().WithMessage("Position doesn't exists.");
            RuleFor(user => user.UserType).IsInEnum().WithMessage("UserType doesn't exists.");

        }
    }
}
