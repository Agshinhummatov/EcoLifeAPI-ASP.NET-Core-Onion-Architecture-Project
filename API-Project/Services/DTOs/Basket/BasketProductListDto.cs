using Services.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Basket
{
    public class BasketProductListDto
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public ProductListDto Product { get; set; }
    }
}
