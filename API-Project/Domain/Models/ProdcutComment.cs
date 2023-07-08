using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProdcutComment:BaseEntity
    {
        public string Context { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
