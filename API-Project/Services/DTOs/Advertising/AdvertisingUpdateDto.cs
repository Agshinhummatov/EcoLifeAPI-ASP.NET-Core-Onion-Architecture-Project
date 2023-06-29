using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Advertising
{
    public class AdvertisingUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
