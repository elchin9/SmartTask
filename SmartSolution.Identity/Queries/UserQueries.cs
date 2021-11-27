using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Identity.Auth;
using SmartSolution.Identity.ViewModels;
using SmartSolution.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSolution.Identity.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly SmartSolutionDbContext _context;
        private readonly IMapper _mapper;

        public UserQueries(SmartSolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> FindAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserProfileDto> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (user == null) return null;

            var profile = _mapper.Map<UserProfileDto>(user);

            return profile;
        }


        public async Task<User> GetUserEntityAsync(int? userId)
        {
            var user = await _context.Users
               .Where(u => u.Id == userId)
               .AsNoTracking()
               .SingleOrDefaultAsync();

            if (user == null) return null;

            return user;
        }

        public async Task<string> GetExistingUser(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                return userName;
            }

            return "";
        }

        public async Task<Role> GetRoleAsyncById(int? id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(p => p.Id == id);

            if (role == null) return null;

            return role;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _context.Users
               .AsNoTracking()
               .ToListAsync();

            if (users == null) return null;

            var outputModel = _mapper.Map<IEnumerable<UserDto>>(users);

            return outputModel;
        }
    }
}
