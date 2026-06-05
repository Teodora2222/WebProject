using Contract.Dtos.User;

namespace Gateway.Services
{
    public interface IUserGatewayService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserAsync(int id);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
