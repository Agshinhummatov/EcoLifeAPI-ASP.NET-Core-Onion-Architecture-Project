using System.ComponentModel.DataAnnotations;

namespace Services.DTOs.Account
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
