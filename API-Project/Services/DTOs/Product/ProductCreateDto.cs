using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public int Rates { get; set; }
      
        public string Description { get; set; }

        public int CategoryId { get; set; }


        [Required(ErrorMessage = "Image is required.")]
        public IFormFile? Photo { get; set; }


        [Required(ErrorMessage = "Image is required.")]
        public IFormFile? HoverPhoto { get; set; }



        //public IFormFileCollection Images { get; set; }

        //public List<byte[]>  Image { get; set; }
    }
}
