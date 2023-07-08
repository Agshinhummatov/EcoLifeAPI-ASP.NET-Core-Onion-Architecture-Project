using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Product
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }

        public int? Count { get; set; }

        public decimal? Price { get; set; }

        public int? Rates { get; set; }
     
        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? Photo { get; set; }

        public IFormFile? HoverPhoto { get; set; }
    }
}
