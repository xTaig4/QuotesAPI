using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuoteAPI.Data;
using QuoteAPI.Entities;
using QuoteAPI.Migrations;
using QuoteAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuoteAPI.Services
{
    public class AuthService(QuoteContext context, IConfiguration configuration) : IAuthService
    {

        public async Task<string?> LoginAsync(UserDTO request)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
            {
                return null;
            }

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return CreateToken(user);
        }

        public async Task<User?> RegisterAsync(UserDTO request)
        {
            
            if (await context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }
            var user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetValue<string>("AppSettings:Token")
            ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
