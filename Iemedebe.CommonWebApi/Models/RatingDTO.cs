using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iemedebe.CommonWebApi
{
    public class RatingDTO
    {
        public Guid Id { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public Guid RatedFilmId { get; set; }

        [Required]
        public Guid RatedById { get; set; }

        public FilmDTO RatedFilm { get; set; }

        public UserDTO RatedBy { get; set; }

        public RatingDTO() { }

        public RatingDTO(Rating rating)
        {
            this.Id = rating.Id;
            this.Score = rating.Score;
            this.RatedFilmId = rating.RatedFilm.Id;
            this.RatedById = rating.RatedBy.Id;
        }

        public Rating ToEntity()
        {
            return new Rating()
            {
                Id = this.Id,
                Score = this.Score,
                RatedBy = this.RatedBy.ToEntity(),
                RatedFilm = this.RatedFilm.ToEntity()
            };
        }
    }
}
