using Iemedebe.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Iemedebe.DataAccess
{
    public class IemedebeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Session> Sessions { get; set; }
        //public object Session { get; internal set; }

        public IemedebeContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
               .HasMany(f => f.Ratings)
               .WithOne(r => r.RatedFilm);
        }
    }
}
