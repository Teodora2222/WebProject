using UserService.Domain.DTOs;

namespace UserService.Domain.Services
{
    public interface IRegisterService
    {
        public Task<bool> register(RegisterDto dto);
    }
}
