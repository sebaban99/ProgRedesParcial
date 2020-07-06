using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;
using Iemedebe.CommonWebApi;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmLogic<Film> filmLogic;
        private readonly IUserLogic<User> userLogic;
        private readonly ILogic<Genre> genreLogic;
        private readonly ILogic<Director> directorLogic;

        public FilmsController(IFilmLogic<Film> filmLogic, ILogic<Genre> genreLogic, 
            ILogic<Director> directorLogic, IUserLogic<User> userLogic) : base()
        {
            this.filmLogic = filmLogic;
            this.genreLogic = genreLogic;
            this.directorLogic = directorLogic;
            this.userLogic = userLogic;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            await Task.Yield();
            try
            {
                List<FilmDTO> filmsInDB = new List<FilmDTO>();
                var films = await filmLogic.GetAllAsync().ConfigureAwait(false);
                foreach(Film f in films)
                {
                    filmsInDB.Add(new FilmDTO(f));
                }
                return Ok(filmsInDB);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostAsync([FromBody]FilmDTO model)
        {
            await Task.Yield();
            try
            { 
                var filmToCreate = model.ToEntity();
                filmToCreate.Director = await directorLogic.GetAsync(model.DirectorID).ConfigureAwait(false);
                filmToCreate.Genres = new List<FilmWithGenre>();
                FilmWithGenre fwg = new FilmWithGenre()
                {
                    GenreId = model.MainGenreID,
                    Genre = await genreLogic.GetAsync(model.MainGenreID).ConfigureAwait(false),
                    Film = filmToCreate,
                };
                filmToCreate.Genres.Add(fwg);

                var film = await filmLogic.CreateAsync(filmToCreate).ConfigureAwait(false);
                return Ok(new FilmDTO(film));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            await Task.Yield();
            try
            {
                var film = await filmLogic.GetAsync(id).ConfigureAwait(false);
                if (film == null)
                {
                    return Ok("There is no film with id: " + id);
                }
                return Ok(new FilmDTO(film));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Task.Yield();
            try
            {
                var filmToDelete = await filmLogic.GetAsync(id).ConfigureAwait(false);
                await filmLogic.RemoveAsync(filmToDelete);
                return Ok("Film successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] FilmDTO model)
        {
            await Task.Yield();
            try
            {
                var originalFilm = await filmLogic.GetAsync(id).ConfigureAwait(false);
                model.Id = originalFilm.Id;
                var updatedFilm = model.ToEntity();
                updatedFilm.Director = await directorLogic.GetAsync(model.DirectorID).ConfigureAwait(false);
                var modifiedEntity = await filmLogic.UpdateAsync(updatedFilm, originalFilm);
                return Ok(new FilmDTO(modifiedEntity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[AuthenticationFilter()]
        [HttpPost("{id}/ratings")]
        public async Task<IActionResult> PostRatingAsync([FromBody]RatingDTO ratingDTO)
        {
            await Task.Yield();
            try
            {
                var ratedFilm = await filmLogic.GetAsync(ratingDTO.RatedFilmId).ConfigureAwait(false);
                var ratedByUser = await userLogic.GetAsync(ratingDTO.RatedById).ConfigureAwait(false);
                ratedByUser.Id = ratingDTO.RatedById;
                ratingDTO.RatedBy = new UserDTO(ratedByUser);
                ratingDTO.RatedFilm = new FilmDTO(ratedFilm);
                var rating = ratingDTO.ToEntity();
                var film = await filmLogic.AddRatingAsync(rating).ConfigureAwait(false);
                return Ok(new FilmDTO(film));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Agregar filtrooo
        [HttpPut("{id}/ratings/{idRating}")]
        public async Task<IActionResult> PutRatingAsync(Guid id, Guid idRating, [FromBody]RatingDTO ratingDTO)
        {
            await Task.Yield();
            try
            {
                ratingDTO.Id = idRating;
                var rating = ratingDTO.ToEntity();
                var film = await filmLogic.PutRatingAsync(id,rating).ConfigureAwait(false);
                return Ok(new FilmDTO(film));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[AuthenticationFilter()]
        [HttpDelete("{id}/ratings/{idRating}")]
        public async Task<IActionResult> DeleteRatingAsync(Guid id, Guid idRating)
        {
            await Task.Yield();
            try
            {
                await filmLogic.RemoveRatingAsync(id, idRating).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/genres")]
        public async Task<IActionResult> PostGenreAsync(Guid id, Guid idGenre)
        {
            await Task.Yield();
            try
            {
                var filmInDB = await filmLogic.GetAsync(id).ConfigureAwait(false);
                var genreInDB = await genreLogic.GetAsync(idGenre).ConfigureAwait(false);
                var film = await filmLogic.AddGenreAsync(filmInDB, genreInDB).ConfigureAwait(false);
                return Ok(new FilmDTO(film));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/genres/{idGenre}")]
        public async Task<IActionResult> DeleteGenreAsync(Guid id, Guid idGenre)
        {
            await Task.Yield();
            try
            {
                var filmInDB = await filmLogic.GetAsync(id).ConfigureAwait(false);
                var genreInDB = await genreLogic.GetAsync(idGenre).ConfigureAwait(false);
                await filmLogic.RemoveGenreAsync(filmInDB, genreInDB).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}