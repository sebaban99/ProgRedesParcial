using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public List<Film> FavouriteFilms { get; set; }


        public override string ToString()
        {
            return Nickname;
        }
        public override bool Equals(object obj)
        {
            return this.Nickname.Equals(((User)obj).Nickname);
        }
    }
}
