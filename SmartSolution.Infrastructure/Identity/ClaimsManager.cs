using Microsoft.AspNetCore.Http;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;

namespace SmartSolution.Infrastructure.Identity
{
    public class ClaimsManager : IClaimsManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public int GetCurrentUserId()
        {
            var claim = GetUserClaim(ClaimTypes.NameIdentifier);

            if (!int.TryParse(claim.Value, out var currentUserId))
                throw new AuthenticationException("Can't parse claim value to required type");

            return currentUserId;
        }

        public string GetCurrentUserName()
        {
            var claim = GetUserClaim(ClaimTypes.Name);
            return claim.Value;
        }

        public IEnumerable<Claim> GetUserClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
        }

        public Claim GetUserClaim(string claimType)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException("User is not authenticated");

            var claim = user.FindFirst(claimType);

            if (claim == null)
                throw new AuthenticationException("User does not have required claim");

            return claim;
        }
    }
}
