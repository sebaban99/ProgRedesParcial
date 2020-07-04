using System.ComponentModel.DataAnnotations;

namespace Iemedebe.UserWebApi
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}