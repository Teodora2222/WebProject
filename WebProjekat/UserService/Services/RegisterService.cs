using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Domain.DTOs;
using UserService.Domain.Models;
using UserService.Domain.Services;

namespace UserService.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly AppDbContext context;

        public RegisterService(AppDbContext contX)
        {
            context = contX;
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
    }
}
