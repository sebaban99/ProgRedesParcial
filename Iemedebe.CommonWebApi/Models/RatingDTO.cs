using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iemedebe.CommonWebApi
{
    public class RatingDTO
    {
        public Guid Id { get; set; }

        public int Score { get; set; }

        public FilmDTO RatedFilm { get; set; }

        public UserDTO RatedBy { get; set; }

        public RatingDTO() { }

        public RatingDTO(Rating rating)
        {
            this.Id = rating.Id;
            this.Score = rating.Score;
            this.RatedFilm = new FilmDTO(rating.RatedFilm);
            this.RatedBy = new UserDTO(rating.RatedBy);
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
