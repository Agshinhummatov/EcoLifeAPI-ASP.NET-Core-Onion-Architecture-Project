using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WishlistProduct :BaseEntity
    {
        public int Quantity { get; set; }
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
