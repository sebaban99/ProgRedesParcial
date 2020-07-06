using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.CommonWebApi
{
    public class FilmDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime LaunchDate { get; set; }

        public DateTime AdditionDate { get; set; }

        public List<GenreDTO> Genres { get; set; }

        [Required]
        public Guid MainGenreID { get; set; }

        [Required]
        public Guid DirectorID { get; set; }

        public DirectorDTO Director { get; set; }

        public List<RatingDTO> Ratings { get; set; }

        public List<UserDTO> UserFavourites { get; set; }

        public FilmDTO() { }

        public FilmDTO(Film film)
        {
            this.Id = film.Id;
            this.Name = film.Name;
            this.Description = film.Description;
            this.LaunchDate = film.LaunchDate;
            this.AdditionDate = film.AdditionDate;
            if(film.Genres != null && film.Genres.Count != 0)
            {
                this.MainGenreID = film.Genres[0].GenreId;
            }
            if (film.Director != null)
            {
                this.Director = new DirectorDTO(film.Director);
                this.DirectorID = film.Director.Id;
            }
            this.Genres = new List<GenreDTO>();
            this.Ratings = new List<RatingDTO>();
            if(film.Ratings != null)
            {
                foreach (Rating rating in film.Ratings)
                {
                    var ratingDTO = new RatingDTO(rating);
                    Ratings.Add(ratingDTO);
                }
            }
            if (film.Genres != null)
            {
                foreach (FilmWithGenre fwg in film.Genres)
                {
                    if(fwg.Genre != null)
                    {
                        var genreDTO = new GenreDTO(fwg.Genre);
                        Genres.Add(genreDTO);
                    }
                }
            }
        }

        public Film ToEntity()
        {
            var film = new Film()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                LaunchDate = this.LaunchDate,
                AdditionDate = this.AdditionDate,
                Ratings = new List<Rating>(),
                Genres = new List<FilmWithGenre>(),
                UserFavourites = new List<UserFavouriteFilm>()
            };

            if (this.Director != null)
            {
                film.Director = this.Director.ToEntity();
            }

            if(Ratings != null)
            {
                foreach (RatingDTO ratingDTO in this.Ratings)
                {
                    film.Ratings.Add(ratingDTO.ToEntity());
                }
            }

            if(Genres != null)
            {
                foreach (GenreDTO genreDTO in this.Genres)
                {
                    FilmWithGenre fwg = new FilmWithGenre()
                    {
                        Genre = genreDTO.ToEntity(),
                        GenreId = genreDTO.Id,
                        Film = film,
                        FilmId = film.Id
                    };
                    film.Genres.Add(fwg);
                }
            }

            if (UserFavourites != null)
            {
                foreach (UserDTO userDTO in this.UserFavourites)
                {
                    UserFavouriteFilm uff = new UserFavouriteFilm()
                    {
                        User = userDTO.ToEntity(),
                        UserId = userDTO.Id,
                        Film = film,
                        FilmId = film.Id
                    };
                    film.UserFavourites.Add(uff);
                }
            }


            return film;
        }
    }
}
