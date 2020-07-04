using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Iemedebe.Domain;

namespace Iemedebe.AdminWebApi
{
    public class DirectorDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Description { get; set; }

        public DirectorDTO() { }

        public DirectorDTO(Director director)
        {
            this.Id = director.Id;
            this.Name = director.Name;
            this.Description = director.Description;
            this.Birthday = director.Birthday;
            this.Gender = director.Gender == Domain.Gender.Female ? "female" : "male";
        }

        public Director ToEntity()
        {
            return new Director()
            {
                Name = this.Name,
                Id = this.Id,
                Description = this.Description,
                Birthday = this.Birthday,
                Gender = this.Gender.Equals("male") ? Domain.Gender.Male : Domain.Gender.Female
            };
        }

    }
}
