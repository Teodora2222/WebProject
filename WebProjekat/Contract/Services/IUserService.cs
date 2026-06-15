using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.User;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface IUserService : IService
    {
        Task<string?> login(LoginDto dto);

        Task<bool> register(RegisterDto dto);

        Task<List<UserDto>> getAllUsers();

        Task<bool> deleteUser(int id);

        Task<UserDto?> getUser(int id);

        Task<bool> updateUser(int id, UpdateUserDto dto);
    }
}
