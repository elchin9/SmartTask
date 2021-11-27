using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.TaskAggregate;
using SmartSolution.Infrastructure.Database;
using SmartSolution.SharedKernel.Infrastructure;

namespace SmartSolution.Infrastructure.Repositories
{
    public class CrmTaskRepository : Repository<CrmTask>, ICrmTaskRepository
    {
        public sealed override DbContext Context { get; protected set; }

        public CrmTaskRepository(SmartSolutionDbContext context)
        {
            Context = context;
        }
    }
}
