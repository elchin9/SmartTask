using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.Infrastructure.Database
{
    public class SmartSolutionDbContextDesignFactory : IDesignTimeDbContextFactory<SmartSolutionDbContext>
    {
        public SmartSolutionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SmartSolutionDbContext>()
                .UseSqlServer("Server=.;Initial Catalog=SmartSolutionCRM;Integrated Security=true", sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("SmartSolution.Infrastructure.Migrations");
                });

            return new SmartSolutionDbContext(optionsBuilder.Options, new NoMediator());
        }

        private class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
                CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
