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
        public DbSet<FilmFile> FilmFiles { get; set; }
        public DbSet<UserFavouriteFilm> UsersFavouriteFilms { get; set; }
        public DbSet<FilmWithGenre> FilmsWithGenres { get; set; }

        //public object Session { get; internal set; }

        public IemedebeContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
               .HasMany(f => f.Ratings)
               .WithOne(r => r.RatedFilm);

            modelBuilder.Entity<UserFavouriteFilm>()
                .HasKey(uf => new { uf.FilmId, uf.UserId });
            modelBuilder.Entity<UserFavouriteFilm>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.FavouriteFilms)
                .HasForeignKey(uf => uf.UserId);
            modelBuilder.Entity<UserFavouriteFilm>()
                .HasOne(uf => uf.Film)
                .WithMany(f => f.UserFavourites)
                .HasForeignKey(uf => uf.FilmId);

            modelBuilder.Entity<FilmWithGenre>()
                .HasKey(uf => new { uf.FilmId, uf.GenreId });
            modelBuilder.Entity<FilmWithGenre>()
                .HasOne(uf => uf.Genre)
                .WithMany(g => g.FilmsAssociated)
                .HasForeignKey(uf => uf.GenreId);
            modelBuilder.Entity<FilmWithGenre>()
                .HasOne(uf => uf.Film)
                .WithMany(f => f.Genres)
                .HasForeignKey(uf => uf.FilmId);
        }
    }
}
