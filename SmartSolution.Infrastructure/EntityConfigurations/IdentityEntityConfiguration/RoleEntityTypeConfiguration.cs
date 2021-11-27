using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Infrastructure.Database;

namespace SmartSolution.Infrastructure.EntityConfigurations.IdentityEntityConfiguration
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> roleConfiguration)
        {
            roleConfiguration.ToTable("Roles", SmartSolutionDbContext.IDENTITY_SCHEMA);

            roleConfiguration.HasKey(o => o.Id);

            roleConfiguration.Property(o => o.Id).UseHiLo($"{nameof(Role)}Seq", SmartSolutionDbContext.IDENTITY_SCHEMA);

            var rolesNavigation = roleConfiguration.Metadata.FindNavigation(nameof(Role.Users));
            rolesNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);


            roleConfiguration.HasIndex(o => o.Name).IsUnique();

            roleConfiguration.Property(o => o.Name).HasMaxLength(100).IsRequired();
        }
    }
}
