using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.Trip;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface ITravelPlanService : IService
    {
         Task<List<TravelPlanDto>> getAllTravelPlans(int userId);

         Task<TravelPlanDto> getTravelPlan(int id, int userId);

         Task<bool> deleteTravelPlan(int id, int userId);

         Task<TravelPlanDto> createTravelPlan(CreateTravelPlanDto dto, int userId);

         Task<bool> updateTravelPlan(int id, UpdateTravelPlanDto dto, int userId);

         Task<List<TravelPlanDto>> getAllTravelPlansAdmin();

         Task<TravelPlanDto?> getTravelPlanById(int id);


    }
}
