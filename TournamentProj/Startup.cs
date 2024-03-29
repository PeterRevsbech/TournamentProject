using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.Mappers;
using TournamentProj.Mappers.MatchDependencyMapper;
using TournamentProj.Services.DrawService;
using TournamentProj.Services.MatchDependencyService;
using TournamentProj.Services.MatchService;
using TournamentProj.Services.PlayerService;
using TournamentProj.Services.TournamentService;

namespace TournamentProj
{
    public class Startup
    {
        private readonly string _myAllowSpecificOrigins = "MyAllow";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: _myAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:3000","https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        ;
                });
            });
            
            services.AddDbContext<TournamentContext>(opt =>
                opt.UseInMemoryDatabase("Tournament"));
            
            //Dependency Injection -----------------------------------------------------------------------------------
            //DBContext
            services.AddScoped<ITournamentContext>(provider => provider.GetService<TournamentContext>());
            
            //Services
            services.AddScoped<ITournamentService,TournamentService>();
            services.AddScoped<IDrawService,DrawService>();
            services.AddScoped<IPlayerService,PlayerService>();
            services.AddScoped<IMatchService,MatchService>();
            services.AddScoped<IMatchDependencyService,MatchDependencyService>();
            
            //Mappers
            services.AddScoped<ITournamentMapper,TournamentMapper>();
            services.AddScoped<IDrawMapper,DrawMapper>();
            services.AddScoped<IPlayerMapper,PlayerMapper>();
            services.AddScoped<IMatchMapper,MatchMapper>();
            services.AddScoped<IMatchDependencyMapper,MatchDependencyMapper>();
            
            //Repositories
            services.AddScoped<ITournamentRepository,TournamentRepository>();
            services.AddScoped<IDrawRepository,DrawRepository>();
            services.AddScoped<IPlayerRepository,PlayerRepository>();
            services.AddScoped<IMatchRepository,MatchRepository>();
            services.AddScoped<IMatchDependencyRepository,MatchDependencyRepository>();
            
            //Other
            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TournamentProj", Version = "v1"});
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TournamentProj v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(_myAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}