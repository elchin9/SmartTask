using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Infrastructure.Database;
using SmartSolution.SharedKernel.Infrastructure;

namespace SmartSolution.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public sealed override DbContext Context { get; protected set; }

        public RoleRepository(SmartSolutionDbContext context)
        {
            Context = context;
        }
    }
}
