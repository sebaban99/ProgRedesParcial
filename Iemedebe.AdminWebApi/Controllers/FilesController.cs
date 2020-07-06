using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.Domain;
using System.IO;
using Iemedebe.BusinessLogic;
using Iemedebe.DataAccess;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public readonly IWebHostEnvironment environment;
        public readonly FilmFileLogic filmFileLogic;
        private readonly IRepository<Film> filmRepository;
        private readonly IRepository<FilmFile> filmFileRepository;

        public FilesController(IWebHostEnvironment environment, IRepository<Film> filmRepository, IRepository<FilmFile> filmFileRepository)
        {
            this.environment = environment;
            filmFileLogic = new FilmFileLogic(filmRepository, filmFileRepository);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllFile()
        {
            await Task.Yield();
            try
            {
                var filesInDB = await filmFileLogic.GetAllFiles().ConfigureAwait(false);
                return Ok(filesInDB);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

        [HttpPost("{id}")]
        public async Task<IActionResult> PostFile(FileToUpload file, Guid idFilm)
        {
            await Task.Yield();
            try
            {
                if(((file.files.Length / 1024f) / 1024f ) > 100)
                {
                    return BadRequest("Error in file upload. File can't be greater than 100 mb corrupt or invalid");
                }
                else if(file.files.Length != 0)
                {
                    if(!Directory.Exists(environment.WebRootPath + "\\UploadedFiles\\"))
                    {
                        Directory.CreateDirectory(environment.WebRootPath + "\\UploadedFiles\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(environment.WebRootPath + "\\UploadedFiles\\" + file.files.FileName))
                    {
                        file.files.CopyTo(fileStream);
                        fileStream.Flush();

                        FilmFile newFilmFile = new FilmFile()
                        {
                            Id = Guid.NewGuid(),
                            Name = file.files.FileName,
                            UploadDate = DateTime.Now,
                            Size = ((file.files.Length / 1024f) / 1024f),
                        };
                        var createdFile =  await filmFileLogic.CreateAsync(idFilm, newFilmFile).ConfigureAwait(false);

                        return Ok(createdFile);
                    }
                }
                else
                {
                    return BadRequest("Error in file upload. File was corrupt or invalid");
                }
            }
            catch(Exception)
            {
                return BadRequest("Error in file upload. File was corrupt or invalid");
            }
        }
    }
}