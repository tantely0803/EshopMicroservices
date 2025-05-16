﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasConversion(
                 Customer => Customer.Value,
                 dbId => CustomerId.Of(dbId));

            builder.Property( c => c.Name ).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(255);

            builder.HasIndex( c => c.Email).IsUnique();
        }
    }
}
