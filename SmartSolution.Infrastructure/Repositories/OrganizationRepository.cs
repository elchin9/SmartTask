using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Infrastructure.Database;
using SmartSolution.SharedKernel.Infrastructure;

namespace SmartSolution.Infrastructure.Repositories
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public sealed override DbContext Context { get; protected set; }

        public OrganizationRepository(SmartSolutionDbContext context)
        {
            Context = context;
        }
    }
}
