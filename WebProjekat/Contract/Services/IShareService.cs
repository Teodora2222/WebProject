using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.Trip;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface IShareService : IService
    {
        Task<ShareResponseDto> CreateShare(int travelPlanId, CreateShareDto dto);

        Task<SharedTravelPlanDto?> GetPermissionFromToken(string token);

        Task<SharedTravelPlanDto?> GetShareByToken(string token);
    }
}
