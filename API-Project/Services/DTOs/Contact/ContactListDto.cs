using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Contact
{
    public class ContactListDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
