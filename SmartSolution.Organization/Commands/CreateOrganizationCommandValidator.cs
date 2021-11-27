using FluentValidation;

namespace SmartSolution.Organization.Commands
{
    public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationCommandValidator() : base()
        {
            RuleFor(command => command.Name).NotNull();
            RuleFor(command => command.PhoneNumber).NotNull();
            RuleFor(command => command.Address).NotNull();
            RuleFor(command => command.Password).NotNull();
            RuleFor(command => command.Email).NotNull();
            RuleFor(command => command.FirstName).NotNull();
        }
    }
}