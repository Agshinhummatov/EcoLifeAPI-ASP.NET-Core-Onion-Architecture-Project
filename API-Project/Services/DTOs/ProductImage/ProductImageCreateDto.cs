using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.ProductImage
{
    public class ProductImageCreateDto
    {
        public IFormFileCollection Images { get; set; }
    }
}
