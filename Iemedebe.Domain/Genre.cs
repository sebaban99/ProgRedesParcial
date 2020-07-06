using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.Domain
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FilmWithGenre> FilmsAssociated { get; set; }
        public override string ToString()
        {
            return Name + ":" + Description;
        }
    }
}
