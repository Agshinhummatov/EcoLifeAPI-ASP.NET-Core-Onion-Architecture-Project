using Services.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Product
{
    public class ProductListDto
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public int Rates { get; set; }

        public string Description { get; set; }


        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public int BasketCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateDate { get; set; }

        public byte[] Image { get; set; }

        public byte[] HoverImage { get; set; }

        //public List<byte[]> Photo { get; set; }

        //public List<string> Photo { get; set; }

        //public byte[]? Image { get; set; }

    }
}
