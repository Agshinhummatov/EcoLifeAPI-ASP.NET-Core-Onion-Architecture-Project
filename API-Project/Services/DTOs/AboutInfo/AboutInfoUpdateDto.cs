using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.AboutInfo
{
    public class AboutInfoUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
