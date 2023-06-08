using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Benefit
{
    public class BenefitCreateDto
    {
        public string? Title { get; set; }
        public byte[]? Image { get; set; }
    }
}
