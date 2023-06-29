using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Benefit
{
    public class BenefitUpdateDto
    {

     
        public string? Title { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
