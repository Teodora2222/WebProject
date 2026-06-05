using Contract.Dtos.User;
using Contract.Services;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Services
{
    public class ServiceFabricUserGatewayService : IUserGatewayService
    {
        private IValidatorService CreateProxy() =>
       ServiceProxy.Create<IValidatorService>(new Uri("fabric:/WebProject/ValidatorService"));

        public Task<List<UserDto>> GetAllUsersAsync() =>
            CreateProxy().GetAllUsers();

        public Task<UserDto?> GetUserAsync(int id) =>
            CreateProxy().GetUser(id);

        public Task<bool> UpdateUserAsync(int id, UpdateUserDto dto) =>
            CreateProxy().UpdateUser(id, dto);

        public Task<bool> DeleteUserAsync(int id) =>
            CreateProxy().DeleteUser(id);
    }
}
