using System;
using System.Collections.Generic;
using System.Text;

namespace Iemedebe.Domain
{
    public class UserFavouriteFilm
    {
        public Guid FilmId { get; set; }
        public Film Film { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
