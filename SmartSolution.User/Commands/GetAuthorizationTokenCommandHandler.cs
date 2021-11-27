using MediatR;
using SmartSolution.Identity.Auth;
using SmartSolution.Identity.Queries;
using SmartSolution.SharedKernel.Infrastructure;
using SmartSolution.User.Commands.Models;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.User.Commands
{
    public class GetAuthorizationTokenCommandHandler : IRequestHandler<GetAuthorizationTokenCommand, JwtTokenDto>
    {
        private readonly IUserManager _userManager;
        private readonly IUserQueries _userQueries;

        public GetAuthorizationTokenCommandHandler(IUserManager userManager, IUserQueries userQueries)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
        }

        public async Task<JwtTokenDto> Handle(GetAuthorizationTokenCommand request,
            CancellationToken cancellationToken)
        {
            var userName = request.UserName.ToLower();
            var user = await _userQueries.FindByNameAsync(userName);
            if (user == null || user.PasswordHash != PasswordHasher.HashPassword(userName, request.Password))
                throw new AuthenticationException("Invalid credentials.");

            (string token, DateTime expiresAt) = _userManager.GenerateJwtToken(user);
            return new JwtTokenDto
            {
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}