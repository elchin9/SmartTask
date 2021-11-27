using MediatR;
using SmartSolution.User.Commands.Models;

namespace SmartSolution.User.Commands
{
    public class GetAuthorizationTokenCommand : IRequest<JwtTokenDto>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
