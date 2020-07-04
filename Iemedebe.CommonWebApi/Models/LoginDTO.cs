using System.ComponentModel.DataAnnotations;

namespace Iemedebe.CommonWebApi
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public LoginDTO() { }
    }
}