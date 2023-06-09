using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Count).IsRequired();
            builder.Property(m => m.Price).IsRequired().HasConversion(v => Math.Round(v, 2), v => v);
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.Rates).IsRequired();


            builder.HasQueryFilter(m => !m.SoftDelete);
        }

    }
}
