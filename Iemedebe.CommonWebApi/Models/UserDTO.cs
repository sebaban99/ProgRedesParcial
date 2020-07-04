using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.CommonWebApi
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

        public List<FilmDTO> FavouriteFilms { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            this.Nickname = user.Nickname;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.Birthday = user.Birthday;
            this.Password = user.Password;
            this.FavouriteFilms = new List<FilmDTO>();
            foreach (Film film in user.FavouriteFilms)
            {
                this.FavouriteFilms.Add(new FilmDTO(film));
            }
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
                FavouriteFilms = new List<Film>()
            };

            foreach (FilmDTO filmDTO in this.FavouriteFilms)
            {
                user.FavouriteFilms.Add(filmDTO.ToEntity());
            }

            return user;
        }
    }
}
