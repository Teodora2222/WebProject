using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Contract.Services;
using Contract.Dtos.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;
using UserService.Models;
using Microsoft.Extensions.Configuration;


namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        public UserService(AppDbContext cont,IConfiguration config) 
        {
            context = cont;
            configuration = config;
        }

        public async Task<string?> login(LoginDto dto)
        {
            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.email == dto.email);

            if (existingUser == null)
                return null;

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

            var jwtIssuer = configuration["Jwt:Issuer"] ?? "TravelPlannerApp";

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> register(RegisterDto dto)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(
                u => u.email == dto.email);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    firstName = dto.firstName,
                    lastName = dto.lastName,
                    email = dto.email,
                    passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    role = "user",
                    createdAt = DateTime.UtcNow
                };
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
                return true;
            }
            System.Console.WriteLine("Email is already used.Try again.");
            return false;
        }

        public async Task<bool> deleteUser(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(
                u => u.id == id);

            if(user == null) return false;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true; 
        }

        public async Task<List<UserDto>> getAllUsers()
        {
            return await context.Users
                .Select(u => new UserDto
                {
                    Id = u.id,
                    FirstName = u.firstName,
                    LastName = u.lastName,
                    Email = u.email,
                    Role = u.role
                })
                .ToListAsync();
        }

        public async Task<UserDto?> getUser(int id)
        {
            return await context.Users
                .Where(u => u.id == id)
                .Select(u => new UserDto
                {
                    Id = u.id,
                    FirstName = u.firstName,
                    LastName = u.lastName,
                    Email = u.email,
                    Role = u.role
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> updateUser(int id,UpdateUserDto userDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user != null)
            {
                user.firstName = userDto.firstName ?? "";
                user.lastName = userDto.lastName ?? "";
                user.email = userDto.email ?? "";
                user.role = userDto.role  ?? "";

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
