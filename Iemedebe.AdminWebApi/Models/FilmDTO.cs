using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.AdminWebApi
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
            foreach(Genre genre in film.Genres)
            {
                var genreDTO = new GenreDTO(genre);
                this.Genres.Add(genreDTO);
            }

        }

        public Film ToEntity()
        {
            var film =  new Film()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                LaunchDate = this.LaunchDate,
                AdditionDate = this.AdditionDate,
                Director = this.Director.ToEntity(),
                Genres = new List<Genre>()
            };

            foreach (GenreDTO genreDTO in this.Genres)
            {
                film.Genres.Add(genreDTO.ToEntity());
            }

            return film;
        }
    }
}
