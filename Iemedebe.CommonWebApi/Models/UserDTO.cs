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
            this.Id = user.Id;
            this.Nickname = user.Nickname;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.Birthday = user.Birthday;
            this.Password = user.Password;
            this.FavouriteFilms = new List<FilmDTO>();

            foreach(UserFavouriteFilm uff in user.FavouriteFilms)
            {
                var filmDTO = new FilmDTO(uff.Film);
                FavouriteFilms.Add(filmDTO);
            }
        }

        public User ToEntity()
        {
            var user = new User()
            {
                Id = this.Id,
                Nickname = this.Nickname,
                FullName = this.FullName,
                Email = this.Email,
                Birthday = this.Birthday,
                Password = this.Password,
                FavouriteFilms = new List<UserFavouriteFilm>()
            };

            if (FavouriteFilms != null)
            {
                foreach (FilmDTO filmDTO in this.FavouriteFilms)
                {
                    UserFavouriteFilm uff = new UserFavouriteFilm()
                    {
                        User = user,
                        UserId = user.Id,
                        Film = filmDTO.ToEntity(),
                        FilmId = filmDTO.Id
                    };
                    user.FavouriteFilms.Add(uff);
                }
            }


            return user;
        }
    }
}
