using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Comment
{
    public class CommentListDto
    {

        public int ProductCommentId { get; set; }
        public string Context { get; set; }
        public string UserName { get; set; }
        public string CreatedTime { get; set; }
    }
}
