using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Wishlist :BaseEntity
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public List<WishlistProduct> WishlistProducts { get; set; }

    }
}
