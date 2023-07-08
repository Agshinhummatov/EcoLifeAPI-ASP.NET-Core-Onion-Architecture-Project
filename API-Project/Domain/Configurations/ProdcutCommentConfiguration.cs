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
    public class ProdcutCommentConfiguration : IEntityTypeConfiguration<ProdcutComment>
    {
        public void Configure(EntityTypeBuilder<ProdcutComment> builder)
        {
            builder.Property(m => m.Context).IsRequired();

        }

    }
}
