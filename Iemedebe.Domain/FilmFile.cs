using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.Domain
{
    public class FilmFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public double Size { get; set; }
        public Film Film { get; set; }
        public override string ToString()
        {
            return Name + " | " + Size + " MB" + " | " + UploadDate;
        }
    }
}
