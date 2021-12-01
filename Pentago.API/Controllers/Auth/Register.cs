using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentago.API.Data;
using Pentago.API.Data.Models;

namespace Pentago.API.Controllers.Auth
{
    [Route("/auth/[controller]")]
    public class Register : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public Register(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Model model)
        {
            var (username, email, password) = model;

            var apiKey = Guid.NewGuid().ToString();
            
            var user = new User
            {
                Username = username,
                NormalizedUsername = username.Normalize(),
                Email = email,
                PasswordHash = Sha256Hash(password),
                ApiKeyHash = Sha256Hash(apiKey),
                GlickoRating = 800,
                GlickoRd = 350
            };

            if (await _dbContext.Users.AnyAsync(u => u.Email == email || u.NormalizedUsername == username.Normalize()))
            {
                HttpContext.Response.StatusCode = 409;
                return "Account with this username or email already exists";
            }
            
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return apiKey;
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