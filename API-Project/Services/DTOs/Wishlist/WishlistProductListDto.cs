using Services.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Wishlist
{
    public class WishlistProductListDto
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public int WishlistId { get; set; }
        public ProductListDto Product { get; set; }
    }
}
