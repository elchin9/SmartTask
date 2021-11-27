using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Identity.Auth;
using SmartSolution.Identity.Queries;
using SmartSolution.Infrastructure.Database;
using SmartSolution.User.Commands.Models;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.User.Commands
{
    public class RefreshUserTokenCommandHandler : IRequestHandler<RefreshUserTokenCommand, JwtTokenDto>
    {
        private readonly IUserManager _userManager;
        private readonly IUserQueries _userQueries;
        private readonly IUserRepository _userRepository;
        private readonly SmartSolutionDbContext _context;

        public RefreshUserTokenCommandHandler(IUserManager userManager, IUserQueries userQueries,
            IUserRepository userRepository, SmartSolutionDbContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<JwtTokenDto> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            var splitToken = request.RefreshToken.Split("_");

            var user = await _context.Users.FirstOrDefaultAsync(p => p.RefreshToken == request.RefreshToken);
            await _userRepository.UnitOfWork.SaveChangesAsync();

            if (user == null)
                throw new AuthenticationException("Invalid token.");

            if (Convert.ToDateTime(splitToken[2]) < DateTime.Now)
                throw new AuthenticationException("Token is expired.");

            (string token, DateTime expiresAt) = _userManager.GenerateJwtToken(user);

            return new JwtTokenDto
            {
                Token = token,
                RefreshToken = request.RefreshToken,
                ExpiresAt = expiresAt
            };
        }

    }
}