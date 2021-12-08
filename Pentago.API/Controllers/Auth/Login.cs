using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pentago.API.Controllers.Auth
{
    [Route("/auth/[controller]")]
    public class Login : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Login> _logger;

        public Login(IConfiguration configuration, ILogger<Login> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] LoginModel model)
        {
            var (usernameOrEmail, password) = model;

            await using var connection = new SQLiteConnection(_configuration.GetConnectionString("App"));
            await connection.OpenAsync();

            var command =
                new SQLiteCommand(
                    @"SELECT id, api_key_hash
                    FROM users
                    WHERE (normalized_username = @usernameOrEmail OR email = @usernameOrEmail)
                      AND password_hash = @passwordHash
                    LIMIT 1;",
                    connection);
            command.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail.Normalize());
            command.Parameters.AddWithValue("@passwordHash", Sha256Hash(password));
            _logger.LogInformation(command.CommandText);

            try
            {
                var reader = await command.ExecuteReaderAsync();
                if (!await reader.ReadAsync())
                {
                    _logger.LogInformation("User {User} not found", usernameOrEmail);
                    Response.StatusCode = 404;
                    await connection.CloseAsync();
                    return "User not found";
                }

                var idOrdinal = reader.GetOrdinal("id");
                var apiKeyOrdinal = reader.GetOrdinal("api_key_hash");

                var id = reader.GetInt32(idOrdinal);
                if (!await reader.IsDBNullAsync(apiKeyOrdinal))
                {
                    var apiKeyHash = reader.GetString(apiKeyOrdinal);
                    _logger.LogInformation("User {User} logged in", usernameOrEmail);
                    return apiKeyHash;
                }
            }
            catch (SQLiteException e)
            {
                _logger.LogError(e, e.Message);
                Response.StatusCode = 500;
                await connection.CloseAsync();
                return e.Message;
            }

            return "";
        }

        private static string Sha256Hash(string value)
        {
            using var hash = SHA256.Create();

            return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(item => item.ToString("x2")));
        }

        public record LoginModel(string UsernameOrEmail, string Password);
    }
}