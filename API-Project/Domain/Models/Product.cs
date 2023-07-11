using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public int Rates { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public byte[]  Image { get; set; }

        public byte[] HoverImage { get; set; }

        public List<ProdcutComment> ProdcutComments { get; set; }
        
    }
}
