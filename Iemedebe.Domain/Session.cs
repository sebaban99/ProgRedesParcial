using System;
using System.Collections.Generic;
using System.Text;

namespace Iemedebe.Domain
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public static User LoggedUser { get; set; }
    }
}
