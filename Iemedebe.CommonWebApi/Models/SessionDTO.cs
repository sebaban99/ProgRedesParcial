using Iemedebe.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Iemedebe.CommonWebApi
{
    public class SessionDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public SessionDTO() { }

        public SessionDTO(Session session)
        {
            this.Id = session.Id;
            this.UserId = session.UserId;
        }
    }
}