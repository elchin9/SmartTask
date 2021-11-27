using FluentValidation;

namespace SmartSolution.User.Commands
{
    public class GetAuthorizationTokenCommandValidator : AbstractValidator<GetAuthorizationTokenCommand>
    {
        public GetAuthorizationTokenCommandValidator() : base()
        {
            RuleFor(command => command.UserName).NotNull();
            RuleFor(command => command.Password).NotNull();
        }
    }
}