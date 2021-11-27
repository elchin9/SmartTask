using SmartSolution.Domain.AggregatesModel.UserAggregate;
using System;
using System.Threading.Tasks;

namespace SmartSolution.Identity.Auth
{
    public interface IUserManager
    {
        int GetCurrentUserId();

        string GetCurrentUserName();

        Task<User> GetCurrentUser();

        (string token, DateTime expiresAt) GenerateJwtToken(User user);
    }
}
