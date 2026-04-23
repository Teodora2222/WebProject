using UserService.Domain.DTOs;

namespace UserService.Domain.Services
{
    public interface ILoginService
    {
        public Task<string> login(LoginDto dto);
    }
}
