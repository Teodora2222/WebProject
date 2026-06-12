using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.Trip;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface IActivityService : IService
    {
        public Task<List<ActivityDto>> getAllActivities(int travelId);

        public Task<ActivityDto?> getActivity(int id);

        public Task<List<ActivityDto>> getActivitiesByDate(int travelPlanId, DateTime date);

        public Task<bool> deleteActivity(int id);

        public Task<bool> updateActivity(int id, UpdateActivityDto dto);

        public Task<ActivityDto> createActivity(CreateActivityDto dto, int travelId);
    }
}
