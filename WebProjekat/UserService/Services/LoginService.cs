using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Data;
using UserService.Domain.DTOs;
using UserService.Domain.Services;

namespace UserService.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public LoginService(AppDbContext contX, IConfiguration config)
        {
            context = contX;
            configuration = config;
        }

        public async Task<string> login(LoginDto dto)
        {
            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.email == dto.email);

            if (existingUser == null)
                return  null;

            if (!BCrypt.Net.BCrypt.Verify(dto.password, existingUser.passwordHash))
                return null;

            var claims = new[]
            {
                new Claim("sub", existingUser.id.ToString()),
                new Claim("email", existingUser.email),
                new Claim("role", existingUser.role)
            };

            var jwtKey = configuration["Jwt:Key"]
                ?? "TvojTajniKljucKojiMoraBitiDugacak32Karaktera!";

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var jwtIssuer = configuration["Jwt:Issuer"]?? "TravelPlannerApp";

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}