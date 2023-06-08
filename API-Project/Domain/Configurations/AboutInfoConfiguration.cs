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
    public class AboutInfoConfiguration : IEntityTypeConfiguration<AboutInfo>
    {
        public void Configure(EntityTypeBuilder<AboutInfo> builder)
        {
            builder.Property(m => m.Title).IsRequired(false);
            builder.Property(m => m.Description).IsRequired(false);
            builder.Property(m => m.Image).IsRequired(false);
        }
    }
}
