using System;
using System.Collections.Generic;
using System.Text;

namespace Iemedebe.Domain
{
    public class FilmWithGenre
    {
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public Guid FilmId { get; set; }
        public Film Film { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                FilmWithGenre f = (FilmWithGenre)obj;
                return this.GenreId.Equals(f.GenreId) && this.FilmId.Equals(f.FilmId);
            }
        }
    }
}
