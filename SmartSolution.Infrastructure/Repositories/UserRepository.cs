using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Infrastructure.Database;
using SmartSolution.SharedKernel.Infrastructure;

namespace SmartSolution.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public sealed override DbContext Context { get; protected set; }

        public UserRepository(SmartSolutionDbContext context)
        {
            Context = context;
        }
    }
}

