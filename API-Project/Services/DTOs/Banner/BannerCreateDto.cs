using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.DTOs.Banner
{
    public class BannerCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile? Photo { get; set; }



       

    }
}
