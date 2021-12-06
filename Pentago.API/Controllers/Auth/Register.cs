using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Pentago.API.Controllers.Auth
{
    [Route("/auth/[controller]")]
    public class Register : Controller
    {
        private readonly IConfiguration _configuration;

        public Register(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task Post([FromBody] Model model)
        {
            var (username, email, password) = model;

            await using var connection = new SqliteConnection(_configuration.GetConnectionString("App"));
            await connection.OpenAsync();

            var command =
                new SqliteCommand("SELECT 1 FROM users WHERE email = @email OR normalized_username = @username",
                    connection);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@username", username.Normalize());

            var exists = (long) (command.ExecuteScalar() ?? 0);

            if (exists > 0)
            {
                HttpContext.Response.StatusCode = 409;
                return;
            }

            command.CommandText =
                @"INSERT INTO users (username, normalized_username, email, password_hash, glicko_rating, glicko_rd)
                VALUES (@username, @normalized_username, @email, @password_hash, 800, 350);";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@normalized_username", username.Normalize());
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password_hash", Sha256Hash(password));

            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            HttpContext.Response.StatusCode = 200;
        }

        private static string Sha256Hash(string value)
        {
            var sb = new StringBuilder();

            using var hash = SHA256.Create();

            return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(item => item.ToString("x2")));
        }

        public record Model(string Username, string Email, string Password);
    }
}