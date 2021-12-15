using System.Data.SQLite;
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
            command.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail.ToStandard());
            command.Parameters.AddWithValue("@passwordHash", Util.Sha256Hash(password));

            try
            {
                int id;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                    {
                        _logger.LogInformation("User {User} not found", usernameOrEmail);
                        Response.StatusCode = 404;
                        await connection.CloseAsync();
                        return "User not found";
                    }

                    var idOrdinal = reader.GetOrdinal("id");

                    id = reader.GetInt32(idOrdinal);
                }

                var apiKey = Util.GenerateApiKey();

                command.CommandText = @"UPDATE users SET api_key_hash = @apiKeyHash WHERE id = @id;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@apiKeyHash", Util.Sha256Hash(apiKey));

                await command.ExecuteNonQueryAsync();

                return apiKey;
            }
            catch (SQLiteException e)
            {
                _logger.LogError(e, "POST /auth/register");
                Response.StatusCode = 500;
                await connection.CloseAsync();
                return e.Message;
            }
        }

        public record LoginModel(string UsernameOrEmail, string Password);
    }
}