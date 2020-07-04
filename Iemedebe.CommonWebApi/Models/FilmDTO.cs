using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.CommonWebApi
{
    public class FilmDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime LaunchDate { get; set; }

        [Required]
        public DateTime AdditionDate { get; set; }

        [Required]
        public List<GenreDTO> Genres { get; set; }

        [Required]
        public DirectorDTO Director { get; set; }

        [Required]
        public List<RatingDTO> Ratings { get; set; }

        public FilmDTO() { }

        public FilmDTO(Film film)
        {
            this.Id = film.Id;
            this.Name = film.Name;
            this.Description = film.Description;
            this.LaunchDate = film.LaunchDate;
            this.AdditionDate = film.AdditionDate;
            this.Director = new DirectorDTO(film.Director);
            this.Genres = new List<GenreDTO>();
            this.Ratings = new List<RatingDTO>();
            foreach(Genre genre in film.Genres)
            {
                var genreDTO = new GenreDTO(genre);
                this.Genres.Add(genreDTO);
            }
            foreach(Rating rating in film.Ratings)
            {
                var ratingDTO = new RatingDTO(rating);
                Ratings.Add(ratingDTO);
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
                Director = this.Director.ToEntity(),
                Genres = new List<Genre>(),
                Ratings = new List<Rating>()
            };

            foreach (GenreDTO genreDTO in this.Genres)
            {
                film.Genres.Add(genreDTO.ToEntity());
            }
            foreach(RatingDTO ratingDTO in this.Ratings)
            {
                film.Ratings.Add(ratingDTO.ToEntity());
            }

            return film;
        }
    }
}
