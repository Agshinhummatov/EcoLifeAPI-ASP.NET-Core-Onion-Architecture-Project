using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Comment
{
    public class CommentCreateDto
    {
        public string? Context { get; set; }
        public string? UserName { get; set; }
        public int PordicutId { get; set; }
    }
}
