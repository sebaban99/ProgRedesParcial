using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class FilmFileLogic
    {
        private readonly IRepository<Film> filmRepository;
        private readonly IRepository<FilmFile> filmFileRepository;

        public FilmFileLogic(IRepository<Film> filmRepository, IRepository<FilmFile> filmFileRepository)
        {
            this.filmRepository = filmRepository;
            this.filmFileRepository = filmFileRepository;
        }

        public async Task<FilmFile> CreateAsync(Guid filmId, FilmFile newFile)
        {
            var film = await filmRepository.GetAsync(filmId).ConfigureAwait(false);
            newFile.Film = film;
            filmFileRepository.Add(newFile);
            await filmFileRepository.SaveChangesAsync().ConfigureAwait(false);
            return newFile;
        }

        public async Task<List<FilmFile>> GetAllFiles()
        {
            return await filmFileRepository.GetAllAsync().ConfigureAwait(false);
        }
    }
}
