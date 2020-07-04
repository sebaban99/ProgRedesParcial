using System;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Iemedebe.AdminWebApi
{
    public class UserDTO
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Password { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            this.Nickname = user.Nickname;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.Birthday = user.Birthday;
            this.Password = user.Password;
        }

        public User ToEntity()
        {
            return new User()
            {
                Nickname = this.Nickname,
                FullName = this.FullName,
                Email = this.Email,
                Birthday = this.Birthday,
                Password = this.Password,
                Id = Guid.NewGuid()
            };
        }
    }
}
