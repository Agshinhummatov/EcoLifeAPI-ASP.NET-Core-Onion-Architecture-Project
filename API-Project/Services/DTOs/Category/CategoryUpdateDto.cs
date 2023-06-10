using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public string? Name { get; set; }

        public byte[]? CategoryImage { get; set; }

    }
}
