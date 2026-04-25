using TripService.Domain.DTOs;
using TripService.Domain.Models;

namespace TripService.Domain.Services
{
    public interface ICheckListItemService
    {
        Task<ChecklistItem> CreateCheckListItem(int travelPlanId, CreateChecklistItemDto dto);
        Task<List<ChecklistItem>> GetAllCheckListItems(int travelPlanId);
        Task<bool> ToggleCheckListItem(int id, bool isCompleted, int travelPlanId);
        Task<bool> DeleteCheckListItem(int id,int travelPlanId);
    }
}
