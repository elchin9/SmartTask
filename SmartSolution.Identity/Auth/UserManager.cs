using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Identity.Queries;
using SmartSolution.Infrastructure;
using SmartSolution.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolution.Identity.Auth
{
    public class UserManager : IUserManager
    {
        private readonly IUserQueries _userQueries;
        private readonly SmartSolutionSettings _settings;
        private readonly IClaimsManager _claimsManager;

        public UserManager(IUserQueries userQueries, IOptions<SmartSolutionSettings> settings, IClaimsManager claimsManager)
        {
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
            _claimsManager = claimsManager ?? throw new ArgumentNullException(nameof(claimsManager));
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            _settings = settings.Value;
        }

        public int GetCurrentUserId()
        {
            return _claimsManager.GetCurrentUserId();
        }

        public string GetCurrentUserName()
        {
            return _claimsManager.GetCurrentUserName();
        }

        public async Task<User> GetCurrentUser()
        {
            var currentUserId = GetCurrentUserId();
            return await _userQueries.FindAsync(currentUserId);
        }

        public (string token, DateTime expiresAt) GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.AddRange(_claimsManager.GetUserClaims(user));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.Date.AddDays(1).AddHours(1);

            var token = new JwtSecurityToken(
                _settings.JwtIssuer,
                _settings.JwtIssuer,
                claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return (tokenHandler.WriteToken(token), expiresAt);
        }
    }
}
