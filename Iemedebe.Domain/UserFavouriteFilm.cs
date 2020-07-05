using System;
using System.Collections.Generic;
using System.Text;

namespace Iemedebe.Domain
{
    public class UserFavouriteFilm
    {
        public Guid Id { get; set; }
        public Guid FilmId { get; set; }
        public Film Film { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                UserFavouriteFilm f = (UserFavouriteFilm)obj;
                return this.UserId.Equals(f.UserId) && this.FilmId.Equals(f.FilmId);
            }
        }
    }
}
