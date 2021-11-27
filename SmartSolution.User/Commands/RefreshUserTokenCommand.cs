using MediatR;
using SmartSolution.User.Commands.Models;

namespace SmartSolution.User.Commands
{
    public class RefreshUserTokenCommand : IRequest<JwtTokenDto>
    {
        public string RefreshToken { get; set; }
    }
}

