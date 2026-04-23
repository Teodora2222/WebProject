using UserService.Domain.DTOs;
using UserService.Domain.Models;

namespace UserService.Domain.Services
{
    public interface IUserService
    {
        public Task<List<User>> getAllUsers();

        public Task<bool> deleteUser(int id);

        public Task<User> getUser(int id);

        public Task<bool> updateUser(int id, UpdateUserDto dto);
    }
}
