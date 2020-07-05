using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.CommonWebApi
{
    public class UserDTO
    {
        public Guid Id { get; set; }

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

        public List<FilmDTO> FavouriteFilms { get; set; }

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
            var user = new User()
            {
                Nickname = this.Nickname,
                FullName = this.FullName,
                Email = this.Email,
                Birthday = this.Birthday,
                Password = this.Password,
                Id = Guid.NewGuid(),
            
            };


            return user;
        }
    }
}
