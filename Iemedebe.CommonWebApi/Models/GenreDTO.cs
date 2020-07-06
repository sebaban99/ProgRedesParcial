using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.CommonWebApi
{
    public class GenreDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<FilmDTO> FilmsAssociated { get; set; }

        public GenreDTO() { }

        public GenreDTO(Genre genre)
        {
            this.Id = genre.Id;
            this.Name = genre.Name;
            this.Description = genre.Description;
        }

        public Genre ToEntity()
        {
            var newGenre = new Genre()
            {
                Name = this.Name,
                Id = this.Id,
                Description = this.Description,
                FilmsAssociated = new List<FilmWithGenre>()
            };
           
            return newGenre;
        }
    }
}
