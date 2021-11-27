using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolution.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSolution.Infrastructure.EntityConfigurations.ClientRequestConfiguration
{
    internal class ClientRequestEntityTypeConfiguration
      : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("requests");

            requestConfiguration.HasKey(cr => cr.Id);
            requestConfiguration.Property(cr => cr.Id);
            requestConfiguration.HasIndex(cr => cr.Key).IsUnique();
            requestConfiguration.Property(cr => cr.Key);
            requestConfiguration.Property(cr => cr.Name).IsRequired();
            requestConfiguration.Property(cr => cr.Time);
        }
    }
}
