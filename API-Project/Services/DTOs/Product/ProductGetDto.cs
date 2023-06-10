using Services.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Product
{
    public class ProductGetDto
    {
        public string? Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public int Rates { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public CategoryListDto? Category { get; set; }
    }
}
