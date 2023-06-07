using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Slider
{
     public class SliderCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }

    }
}
