using FluentValidation;

namespace SmartSolution.User.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() : base()
        {
            RuleFor(command => command.Password).NotNull();
            RuleFor(command => command.Email).NotNull();
            RuleFor(command => command.FirstName).NotNull();
        }
    }
}