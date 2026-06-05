using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.Models;

namespace TripService.Services
{
    public class CheckListItemService : ICheckListItemService
    {
        private readonly AppDbContext context;

        public CheckListItemService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<CheckListItemResponseDto> CreateCheckListItem(int travelPlanId, CreateChecklistItemDto dto)
        {
            ChecklistItem checklistItem = new ChecklistItem
            {
                name = dto.Name,
                TravelPlanId = travelPlanId

            };
            await context.ChecklistItems.AddAsync(checklistItem);
            await context.SaveChangesAsync();
            return new CheckListItemResponseDto
            {
                Id = checklistItem.Id,
                TravelPlanId = checklistItem.TravelPlanId,
                Name = checklistItem.name,
                IsCompleted = checklistItem.isCompleted
            };
        }

        public async Task<bool> DeleteCheckListItem(int id, int travelPlanId)
        {
            var checkList = await context.ChecklistItems.FirstOrDefaultAsync(c => c.Id == id
                            && c.TravelPlanId == travelPlanId);
            if (checkList == null) return false;

            context.ChecklistItems.Remove(checkList);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CheckListItemResponseDto>> GetAllCheckListItems(int travelPlanId)
        {
            return await context.ChecklistItems
        .Where(c => c.TravelPlanId == travelPlanId)
        .Select(c => new CheckListItemResponseDto
        {
            Id = c.Id,
            TravelPlanId = c.TravelPlanId,
            Name = c.name,
            IsCompleted = c.isCompleted
        })
        .ToListAsync();
        }

        public async Task<bool> ToggleCheckListItem(int id, bool isCompleted,int travelPlanId)
        {
            var checkList = await context.ChecklistItems.FirstOrDefaultAsync(c => c.Id == id
                        && c.TravelPlanId == travelPlanId);
            if (checkList != null)
            {
                checkList.isCompleted = isCompleted;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
