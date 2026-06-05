using Contract.Dtos.User;
using ValidatorService.Clients;

namespace ValidatorService.Validators
{
    public class UserValidator
    {
        private readonly UserServiceClient userClient;

        public UserValidator(UserServiceClient userClient)
        {
            this.userClient = userClient;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.email))
                return false;

            if (string.IsNullOrWhiteSpace(dto.password))
                return false;

            if (string.IsNullOrWhiteSpace(dto.firstName))
                return false;

            if (string.IsNullOrWhiteSpace(dto.lastName))
                return false;

            return await userClient.CreateProxy().register(dto);
        }

        public async Task<string?> Login(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.email))
                return null;

            if (string.IsNullOrWhiteSpace(dto.password))
                return null;

            return await userClient.CreateProxy().login(dto);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await userClient.CreateProxy().getAllUsers();
        }

        public async Task<UserDto?> GetUser(int id)
        {
            if (id <= 0)
                return null;

            return await userClient.CreateProxy().getUser(id);
        }

        public async Task<bool> DeleteUser(int id)
        {
            if (id <= 0)
                return false;

            return await userClient.CreateProxy().deleteUser(id);
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDto dto)
        {
            if (id <= 0)
                return false;

            return await userClient.CreateProxy().updateUser(id, dto);
        }
    }
}