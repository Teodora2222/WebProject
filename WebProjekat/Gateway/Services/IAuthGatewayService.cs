using Contract.Dtos.User;

namespace Gateway.Services
{
    public interface IAuthGatewayService
    {
        Task<string?> LoginAsync(LoginDto dto);
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}
