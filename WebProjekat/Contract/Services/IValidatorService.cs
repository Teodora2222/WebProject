using Contract.Dtos.Expense;
using Contract.Dtos.Trip;
using Contract.Dtos.User;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface IValidatorService : IService
    {
        // USER

        Task<string?> Login(LoginDto dto);
        Task<bool> Register(RegisterDto dto);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto?> GetUser(int id);
        Task<bool> UpdateUser(int id, UpdateUserDto dto);
        Task<bool> DeleteUser(int id);

        // TRAVEL PLAN

        Task<TravelPlanDto?> CreateTravelPlan(CreateTravelPlanDto dto, int userId);
        Task<bool> UpdateTravelPlan(int id, UpdateTravelPlanDto dto, int userId);
        Task<bool> DeleteTravelPlan(int id, int userId);
        Task<TravelPlanDto?> GetTravelPlan(int id, int userId);
        Task<List<TravelPlanDto>> GetAllTravelPlans(int userId);
        Task<List<TravelPlanDto>> GetAllTravelPlansAdmin();
        Task<TravelPlanDto?> GetTravelPlanById(int id);

        // DESTINATION

        Task<DestinationDto?> CreateDestination(CreateDestinationDto dto, int travelPlanId);
        Task<bool> UpdateDestination(int id, UpdateDestinationDto dto);
        Task<bool> DeleteDestination(int id);
        Task<DestinationDto?> GetDestination(int id);
        Task<List<DestinationDto>> GetAllDestinations(int travelPlanId);

        // ACTIVITY

        Task<ActivityDto?> CreateActivity(CreateActivityDto dto, int travelId);
        Task<bool> UpdateActivity(int id, UpdateActivityDto dto);
        Task<bool> DeleteActivity(int id);
        Task<ActivityDto?> GetActivity(int id);
        Task<List<ActivityDto>> GetAllActivities(int travelId);

        Task<List<ActivityDto>> GetActivitiesByDate(int travelPlanId,DateTime date);

        // CHECKLIST

        Task<CheckListItemResponseDto?> CreateCheckListItem(int travelPlanId, CreateChecklistItemDto dto);
        Task<bool> DeleteCheckListItem(int id, int travelPlanId);
        Task<bool> ToggleCheckListItem(int id, bool isCompleted, int travelPlanId);
        Task<List<CheckListItemResponseDto>> GetAllCheckListItems(int travelPlanId);

        // SHARE

        Task<ShareResponseDto?> CreateShare(int travelPlanId, CreateShareDto dto);
        Task<SharedTravelPlanDto?> GetShareByToken(string token);
        Task<SharedTravelPlanDto?> GetPermissionFromToken(string token);

        // EXPENSE

        Task<ExpenseDto?> CreateExpense(CreateExpenseDto dto, int travelPlanId);
        Task<bool> UpdateExpense(int id, UpdateExpenseDto dto);
        Task<bool> DeleteExpense(int id);
        Task<List<ExpenseDto>> GetAllExpenses(int travelId);
        Task<ExpenseSummaryDto> GetExpenseSummary(int travelId, decimal budget);
    }
}