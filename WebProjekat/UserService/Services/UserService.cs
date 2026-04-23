using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Domain.DTOs;
using UserService.Domain.Models;
using UserService.Domain.Services;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        public UserService(AppDbContext cont) 
        {
            context = cont;
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

        public async Task<List<User>> getAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> getUser(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<bool> updateUser(int id,UpdateUserDto userDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user != null)
            {
                user.firstName = userDto.firstName;
                user.lastName = userDto.lastName;
                user.email = userDto.email;
                user.role = userDto.role;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
