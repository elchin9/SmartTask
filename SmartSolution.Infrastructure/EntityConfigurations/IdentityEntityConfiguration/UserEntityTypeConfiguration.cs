using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Infrastructure.Database;

namespace SmartSolution.Infrastructure.EntityConfigurations.IdentityEntityConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            userConfiguration.ToTable("Users", SmartSolutionDbContext.IDENTITY_SCHEMA);

            userConfiguration.HasKey(o => o.Id);

            userConfiguration.Property(o => o.Id).UseHiLo($"{nameof(User)}Seq", SmartSolutionDbContext.IDENTITY_SCHEMA);


            userConfiguration.HasIndex(o => o.UserName).IsUnique();
            userConfiguration.Property(o => o.UserName).HasMaxLength(100).IsRequired();
            userConfiguration.Property(o => o.FirstName).HasMaxLength(100).IsRequired();
            userConfiguration.Property(o => o.Email).HasMaxLength(100).IsRequired();
            userConfiguration.Property(o => o.PasswordHash).IsRequired();
            userConfiguration.Property(o => o.OrganizationId).IsRequired();
            userConfiguration.Property(o => o.RoleId).IsRequired();
        }
    }
}
