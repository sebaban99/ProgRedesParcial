using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.Domain
{

    public class Film
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime AdditionDate { get; set; }
        public List<Genre> Genres { get; set; }
        public Director Director { get; set; }
        public int FilmScore { get; set; }
        public List<Rating> Ratings { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Film f = (Film)obj;
                return this.Name.Equals(f.Name);
            }
        }
    }
}
