using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.Trip;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface ICheckListItemService : IService
    {
        Task<CheckListItemResponseDto> CreateCheckListItem(int travelPlanId, CreateChecklistItemDto dto);
        Task<List<CheckListItemResponseDto>> GetAllCheckListItems(int travelPlanId);
        Task<bool> ToggleCheckListItem(int id, bool isCompleted, int travelPlanId);
        Task<bool> DeleteCheckListItem(int id, int travelPlanId);
    }
}
