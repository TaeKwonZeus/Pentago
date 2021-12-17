using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pentago.API.Controllers.Auth;

[Route("/auth/[controller]")]
public class Register : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<Register> _logger;

    public Register(IConfiguration configuration, ILogger<Register> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    public async Task Post([FromBody] RegisterModel model)
    {
        var (username, email, password) = model;

        await using var connection = new SqliteConnection(_configuration.GetConnectionString("App"));
        await connection.OpenAsync();

        var command =
            new SqliteCommand("SELECT 1 FROM users WHERE email = @email OR normalized_username = @username",
                connection);
        command.Parameters.AddWithValue("@email", email.ToStandard());
        command.Parameters.AddWithValue("@username", username.ToStandard());

        try
        {
            var exists = Convert.ToInt32(command.ExecuteScalar() ?? 0);
            if (exists > 0)
            {
                Response.StatusCode = 409;
                await connection.CloseAsync();
                return;
            }
        }
        catch (SQLiteException e)
        {
            _logger.LogError(e, "POST /auth/register");
            Response.StatusCode = 500;
            await connection.CloseAsync();
            return;
        }

        command.CommandText =
            @"INSERT INTO users (username, normalized_username, email, password_hash, glicko_rating, glicko_rd)
                VALUES (@username, @normalized_username, @email, @password_hash, 800, 350);";
        command.Parameters.Clear();
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@normalized_username", username.ToStandard());
        command.Parameters.AddWithValue("@email", email.ToStandard());
        command.Parameters.AddWithValue("@password_hash", Util.Sha256Hash(password));

        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SqliteException e)
        {
            _logger.LogError(e, "POST /auth/register");
            Response.StatusCode = 500;
            await connection.CloseAsync();
            return;
        }

        await connection.CloseAsync();

        _logger.LogInformation("User {User} registered", model.Username);
    }

    public record RegisterModel(string Username, string Email, string Password);
}