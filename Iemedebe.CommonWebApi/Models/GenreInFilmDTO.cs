using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iemedebe.CommonWebApi
{
    public class GenreInFilmDTO
    {
        public Guid Id { get; set; }

        [Required]
        public Guid FilmId { get; set; }

        [Required]
        public Guid GenreId { get; set; }


        public GenreInFilmDTO() { }

        public GenreInFilmDTO(FilmWithGenre genre)
        {
            this.FilmId = genre.FilmId;
            this.GenreId = genre.GenreId;
            this.Id = genre.Id;
        }

        public FilmWithGenre ToEntity()
        {
            return new FilmWithGenre()
            {
                Id = this.Id,
                FilmId = this.FilmId,
                GenreId = this.GenreId,
            };
        }
    }
}
