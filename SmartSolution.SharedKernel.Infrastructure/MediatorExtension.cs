using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartSolution.SharedKernel.Domain.Seedwork;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSolution.SharedKernel.Infrastructure
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

            var tasks = domainEvents.Select(async @event => await mediator.Publish(@event));

            await Task.WhenAll(tasks);
        }
    }
}
