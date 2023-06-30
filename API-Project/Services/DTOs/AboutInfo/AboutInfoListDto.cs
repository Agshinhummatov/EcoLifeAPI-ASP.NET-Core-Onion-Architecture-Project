using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.AboutInfo
{
    public class AboutInfoListDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
