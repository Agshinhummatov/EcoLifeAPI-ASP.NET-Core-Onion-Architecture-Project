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
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(m => m.Name).HasMaxLength(255).IsRequired(false);
            builder.Property(m => m.Email).IsRequired(false);
            builder.Property(m => m.Content).IsRequired(false);
            builder.Property(m => m.Subject).IsRequired(false);
        }
    }
}
