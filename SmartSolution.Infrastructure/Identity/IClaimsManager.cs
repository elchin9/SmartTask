using SmartSolution.Domain.AggregatesModel.UserAggregate;
using System.Collections.Generic;
using System.Security.Claims;

namespace SmartSolution.Infrastructure.Identity
{
    public interface IClaimsManager
    {
        int GetCurrentUserId();

        string GetCurrentUserName();

        IEnumerable<Claim> GetUserClaims(User user);

        Claim GetUserClaim(string claimType);
    }
}
