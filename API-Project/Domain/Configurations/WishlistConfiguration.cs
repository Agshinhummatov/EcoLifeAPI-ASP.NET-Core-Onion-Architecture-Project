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

    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {

            //builder.Property(m => m.SoftDelete).HasDefaultValue(false);
            //builder.Property(m => m.CreatedAt).HasDefaultValue(DateTime.UtcNow.ToLongDateString());

            builder.HasQueryFilter(m => !m.SoftDelete);
        }

    }
}
