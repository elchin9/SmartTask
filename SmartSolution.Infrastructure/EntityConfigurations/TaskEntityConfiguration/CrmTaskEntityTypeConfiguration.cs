using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolution.Domain.AggregatesModel.TaskAggregate;
using SmartSolution.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSolution.Infrastructure.EntityConfigurations.TaskEntityConfiguration
{
    public class CrmTaskEntityTypeConfiguration : IEntityTypeConfiguration<CrmTask>
    {
        public void Configure(EntityTypeBuilder<CrmTask> userConfiguration)
        {
            userConfiguration.ToTable("Tasks", SmartSolutionDbContext.IDENTITY_SCHEMA);

            userConfiguration.HasKey(o => o.Id);

            userConfiguration.Property(o => o.Id).UseHiLo($"{nameof(CrmTask)}Seq", SmartSolutionDbContext.IDENTITY_SCHEMA);

            var rolesNavigation = userConfiguration.Metadata.FindNavigation(nameof(CrmTask.Employees));
            rolesNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            userConfiguration.HasIndex(o => o.Id).IsUnique();
            userConfiguration.Property(o => o.Title).HasMaxLength(100).IsRequired();
            userConfiguration.Property(o => o.Description).HasMaxLength(500).IsRequired();
        }
    }
}
