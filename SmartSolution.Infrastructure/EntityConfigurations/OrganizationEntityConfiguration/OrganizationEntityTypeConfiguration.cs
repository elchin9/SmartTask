using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSolution.Infrastructure.EntityConfigurations.OrganizationEntityConfiguration
{
    public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> userConfiguration)
        {
            userConfiguration.ToTable("Organizations", SmartSolutionDbContext.IDENTITY_SCHEMA);

            userConfiguration.HasKey(o => o.Id);

            userConfiguration.Property(o => o.Id).UseHiLo($"{nameof(Organization)}Seq", SmartSolutionDbContext.IDENTITY_SCHEMA);

            var rolesNavigation = userConfiguration.Metadata.FindNavigation(nameof(Organization.Users));
            rolesNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            userConfiguration.Property(o => o.Address).HasMaxLength(100).IsRequired();
            userConfiguration.Property(o => o.Name).HasMaxLength(100).IsRequired();
            userConfiguration.Property(o => o.PhoneNumber).HasMaxLength(100).IsRequired();
        }
    }
}
