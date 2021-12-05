using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pentago.Engine;

namespace Pentago.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Pentago.API", Version = "v1"});
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddCors();
            services.AddSingleton(IEngine.Instance(Configuration.GetConnectionString("Engine")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pentago.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options => options.WithOrigins("https://localhost:5001")
                .AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var connection = new SqliteConnection(Configuration.GetConnectionString("App"));
            connection.Open();

            new SqliteCommand(@"CREATE TABLE IF NOT EXISTS users
                (
                    id                  INTEGER PRIMARY KEY ASC,
                    username            VARCHAR(32)        NOT NULL,
                    normalized_username VARCHAR(32) UNIQUE NOT NULL,
                    email               VARCHAR(32) UNIQUE NOT NULL,
                    password_hash       CHAR(64)           NOT NULL,
                    api_key_hash        CHAR(64),
                    glicko_rating       INT                NOT NULL,
                    glicko_rd           REAL               NOT NULL
                );

                CREATE TABLE IF NOT EXISTS games
                (
                    id        INTEGER PRIMARY KEY ASC,
                    white     INT NOT NULL,
                    black     INT NOT NULL,
                    game_data TEXT,
                    FOREIGN KEY (white) REFERENCES users (id),
                    FOREIGN KEY (black) REFERENCES users (id)
                );", connection).ExecuteNonQuery();

            connection.Close();
        }
    }
}