using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Benefit
{
    public class BenefitListDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
