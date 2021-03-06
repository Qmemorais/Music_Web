using BusinessLayer.Services;
using BusinessLayer.Services.Interface;
using DataLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web_Music
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {//add db to program
            services.AddDbContext<MusicContext>(options => 
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=musicdb;Trusted_Connection=True;"));
            //add automapper to protect models
            services.AddAutoMapper(typeof(Startup).Assembly);
            //add services to work with models
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            //add controllers
            services.AddControllers();
            //add swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
