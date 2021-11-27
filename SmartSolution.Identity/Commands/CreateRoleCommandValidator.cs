using FluentValidation;

namespace SmartSolution.Identity.Commands
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator() : base()
        {
            RuleFor(command => command.Name).NotNull();
        }
    }
}
