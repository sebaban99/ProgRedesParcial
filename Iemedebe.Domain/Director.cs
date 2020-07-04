using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.Domain
{
    public enum Gender
    {
        Female, Male
    }

    public class Director
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + ":" + Gender + ":" + Birthday.ToShortDateString() + ":" + Description;
        }
    }
}
