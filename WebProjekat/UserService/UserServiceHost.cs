using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Contract.Services;
using Contract.Dtos.User;


namespace UserService
{
    internal sealed class UserServiceHost : StatelessService, IUserService
    {
        private readonly IServiceProvider serviceProvider;
        public UserServiceHost(StatelessServiceContext context, IServiceProvider serviceProvider)
            : base(context) 
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<bool> deleteUser(int id)
        {
            return ExecuteAsync(us => us.deleteUser(id));
        }

        public Task<List<UserDto>> getAllUsers()
        {
            return ExecuteAsync(us => us.getAllUsers());
        }

        public Task<UserDto> getUser(int id)
        {
            return ExecuteAsync(us => us.getUser(id));
        }

        public Task<string> login(LoginDto dto)
        {
            return ExecuteAsync(us => us.login(dto));
        }

        public Task<bool> register(RegisterDto dto)
        {
            return ExecuteAsync(us => us.register(dto));
        }

        public Task<bool> updateUser(int id, UpdateUserDto dto)
        {
            return ExecuteAsync(us => us.updateUser(id, dto));
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners(); 
        }

        private async Task<T> ExecuteAsync<T>(Func<IUserService, Task<T>> action)
        {
            using var scope = serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            return await action(userService);
        }
    }
}