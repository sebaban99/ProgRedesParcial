using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.Domain
{
    public class Rating
    {
        public int Score { get; set; }

        public Film RatedFilm { get; set; }

        public User RatedBy { get; set; }

    }
}
