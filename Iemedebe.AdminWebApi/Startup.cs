using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.BusinessLogic;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Iemedebe.AdminWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //AddScoped Logic
            services.AddScoped<IFilmLogic<Film>, FilmLogic>();
            services.AddScoped<ISessionLogic<Session>, SessionLogic>();
            services.AddScoped<IUserLogic<User>, UserLogic>();
            services.AddScoped<ILogic<Director>, DirectorLogic>();
            services.AddScoped<ILogic<Genre>, GenreLogic>();
            services.AddScoped<ILogic<Film>, FilmLogic>();
            services.AddScoped<ILogic<Rating>, RatingLogic>();

            services.AddScoped<IUserValidator<User>, UserValidator>();
            services.AddScoped<IValidator<Director>, DirectorValidator>();
            services.AddScoped<IFilmValidator<Film>, FilmValidator>();
            services.AddScoped<IValidator<Rating>, RatingValidator>();
            services.AddScoped<IValidator<Genre>, GenreValidator>();

            //AddScoped Repository
            services.AddScoped<IRepository<User>, UserRespository>();
            services.AddScoped<IRepository<Director>, DirectorRepository>();
            services.AddScoped<IRepository<Genre>, GenreRepository>();
            services.AddScoped<IRepository<Film>, FilmRepository>();
            services.AddScoped<IRepository<FilmFile>, FilmFileRepository>();
            services.AddScoped<IRepository<Rating>, RatingRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();

            services.AddDbContext<DbContext, IemedebeContext>(
               o => o.UseSqlServer(Configuration.GetConnectionString("Iemedebe")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
