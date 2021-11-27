using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Identity.ViewModels;
using SmartSolution.SharedKernel.Infrastructure.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSolution.Identity.Queries
{
    public interface IUserQueries : IQuery
    {
        Task<User> FindByNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindAsync(int userId);

        Task<UserProfileDto> GetUserProfileAsync(int userId);

        Task<User> GetUserEntityAsync(int? userId);

        Task<string> GetExistingUser(string userName);

        Task<Role> GetRoleAsyncById(int? id);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
