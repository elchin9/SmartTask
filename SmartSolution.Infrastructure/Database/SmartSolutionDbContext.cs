using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.TaskAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Infrastructure.EntityConfigurations.ClientRequestConfiguration;
using SmartSolution.Infrastructure.EntityConfigurations.IdentityEntityConfiguration;
using SmartSolution.Infrastructure.EntityConfigurations.OrganizationEntityConfiguration;
using SmartSolution.SharedKernel.Domain.Seedwork;
using SmartSolution.SharedKernel.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.Infrastructure.Database
{
    public sealed class SmartSolutionDbContext : DbContext, IUnitOfWork
    {
        public const string IDENTITY_SCHEMA = "Identity";
        public const string DEFAULT_SCHEMA = "dbo";

        private readonly IMediator _mediator;

        public SmartSolutionDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<Organization> Organization { get; private set; }
        public DbSet<CrmTask> CrmTasks { get; private set; }


        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await SaveChangesAsync(true, cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }
    }
}
