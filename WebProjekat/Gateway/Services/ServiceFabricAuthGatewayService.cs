using Contract.Dtos.User;
using Contract.Services;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Services
{
    public class ServiceFabricAuthGatewayService : IAuthGatewayService
    {
        private IValidatorService CreateProxy() =>
            ServiceProxy.Create<IValidatorService>(new Uri("fabric:/WebProject/ValidatorService"));

        public Task<string?> LoginAsync(LoginDto dto)
        {
            try
            {
                return  CreateProxy().Login(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public Task<bool> RegisterAsync(RegisterDto dto) =>
            CreateProxy().Register(dto);
    }
}
