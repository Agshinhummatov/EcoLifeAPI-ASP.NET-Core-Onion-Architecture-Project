
using System.ComponentModel.DataAnnotations;

namespace Services.DTOs.Account
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
