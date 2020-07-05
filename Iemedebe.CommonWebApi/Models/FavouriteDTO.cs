using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Iemedebe.Domain;

namespace Iemedebe.CommonWebApi
{
    public class FavouriteDTO
    {
        public Guid Id { get; set; }

        [Required]
        public Guid FilmId { get; set; }

        public Guid UserId { get; set; }


        public FavouriteDTO() { }

        public FavouriteDTO(UserFavouriteFilm favourite)
        {
            this.FilmId = favourite.FilmId;
            this.UserId = favourite.UserId;
            this.Id = favourite.Id;
        }

        public UserFavouriteFilm ToEntity()
        {
            return new UserFavouriteFilm()
            {
                Id = this.Id,
                FilmId = this.FilmId,
                UserId = this.UserId,
            };
        }
    }
}
