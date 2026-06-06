using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contract.Dtos.Expense;
using Contract.Dtos.Trip;
using Contract.Dtos.User;
using Contract.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ValidatorService.Validators;

namespace ValidatorService
{
    internal sealed class ValidatorService : StatelessService,IValidatorService
    {
        private readonly UserValidator userValidator;
        private readonly ExpenseValidator expenseValidator;
        private readonly TravelValidator travelValidator;


        public ValidatorService(StatelessServiceContext context,UserValidator userValidator,TravelValidator travelValidator,
           ExpenseValidator expenseValidator)
           : base(context)
        {
            this.userValidator = userValidator;
            this.travelValidator = travelValidator;
            this.expenseValidator = expenseValidator;
        }

        public Task<ActivityDto?> CreateActivity(CreateActivityDto dto, int travelId)
        {
            return travelValidator.CreateActivity(dto, travelId);
        }

        public Task<CheckListItemResponseDto?> CreateCheckListItem(int travelPlanId, CreateChecklistItemDto dto)
        {
            return travelValidator.CreateCheckListItem(travelPlanId, dto);
        }

        public Task<DestinationDto?> CreateDestination(CreateDestinationDto dto, int travelPlanId)
        {
            return travelValidator.CreateDestination(dto, travelPlanId);
        }

        public Task<ExpenseDto?> CreateExpense(CreateExpenseDto dto, int travelPlanId)
        {
            return expenseValidator.CreateExpense(dto, travelPlanId);
        }

        public Task<ShareResponseDto?> CreateShare(int travelPlanId, CreateShareDto dto)
        {
            return travelValidator.CreateShare(travelPlanId,dto);
        }

        public Task<TravelPlanDto?> CreateTravelPlan(CreateTravelPlanDto dto, int userId)
        {
            return travelValidator.CreateTravelPlan(dto, userId);   
        }

        public Task<bool> DeleteActivity(int id)
        {
            return travelValidator.DeleteActivity(id);
        }

        public Task<bool> DeleteCheckListItem(int id, int travelPlanId)
        {
            return travelValidator.DeleteCheckListItem(id, travelPlanId);
        }

        public Task<bool> DeleteDestination(int id)
        {
            return travelValidator.DeleteDestination(id);
        }

        public Task<bool> DeleteExpense(int id)
        {
            return expenseValidator.DeleteExpense(id);
        }

        public Task<bool> DeleteExpensesByTravelPlan(int travelPlanId)
        {
           return expenseValidator.DeleteExpensesByTravelPlan(travelPlanId);
        }

        public Task<bool> DeleteTravelPlan(int id, int userId)
        {
            return travelValidator.DeleteTravelPlan(id, userId);
        }

        public Task<bool> DeleteTravelPlanAdmin(int id)
        {
            return travelValidator.DeleteTravelPlanAdmin(id);
        }

        public Task<bool> DeleteUser(int id)
        {
            return userValidator.DeleteUser(id);
        }

        public Task<List<ActivityDto>> GetActivitiesByDate(int travelPlanId, DateTime date)
        {
           return travelValidator.GetActivitiesByDate(travelPlanId, date);
        }

        public Task<ActivityDto?> GetActivity(int id)
        {
            return travelValidator.GetActivity(id);
        }

        public Task<List<ActivityDto>> GetAllActivities(int travelId)
        {
            return travelValidator.GetAllActivities(travelId);
        }

        public Task<List<CheckListItemResponseDto>> GetAllCheckListItems(int travelPlanId)
        {
            return travelValidator.GetAllCheckListItems(travelPlanId);
        }

        public Task<List<DestinationDto>> GetAllDestinations(int travelPlanId)
        {
            return travelValidator.GetAllDestinations(travelPlanId);
        }

        public Task<List<ExpenseDto>> GetAllExpenses(int travelId)
        {
            return expenseValidator.GetAllExpenses(travelId);
        }

        public Task<List<TravelPlanDto>> GetAllTravelPlans(int userId)
        {
            return travelValidator.GetAllTravelPlans(userId);
        }

        public Task<List<TravelPlanDto>> GetAllTravelPlansAdmin()
        {
            return travelValidator.GetAllTravelPlansAdmin();
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            return userValidator.GetAllUsers();
        }

        public Task<DestinationDto?> GetDestination(int id)
        {
            return travelValidator.GetDestination(id);
        }

        public Task<ExpenseSummaryDto> GetExpenseSummary(int travelId, decimal budget)
        {
            return expenseValidator.GetExpenseSummary(travelId, budget);
        }

        public Task<SharedTravelPlanDto?> GetPermissionFromToken(string token)
        {
            return travelValidator.GetPermissionFromToken(token);
        }

        public Task<SharedTravelPlanDto?> GetShareByToken(string token)
        {
            return travelValidator.GetShareByToken(token);
        }

        public Task<TravelPlanDto?> GetTravelPlan(int id, int userId)
        {
            return travelValidator.GetTravelPlan(id, userId);
        }

        public Task<TravelPlanDto?> GetTravelPlanById(int id)
        {
            return travelValidator.GetTravelPlanById(id);
        }

        public Task<UserDto?> GetUser(int id)
        {
            return userValidator.GetUser(id);
        }

        public Task<string?> Login(LoginDto dto)
        {
            return userValidator.Login(dto);
        }

        public Task<bool> Register(RegisterDto dto)
        {
            return userValidator.Register(dto);
        }

        public Task<bool> ToggleCheckListItem(int id, bool isCompleted, int travelPlanId)
        {
            return travelValidator.ToggleCheckListItem(id, isCompleted, travelPlanId);
        }

        public Task<bool> UpdateActivity(int id, UpdateActivityDto dto)
        {
            return travelValidator.UpdateActivity(id, dto);
        }

        public Task<bool> UpdateDestination(int id, UpdateDestinationDto dto)
        {
            return travelValidator.UpdateDestination(id, dto);
        }

        public Task<bool> UpdateExpense(int id, UpdateExpenseDto dto)
        {
            return expenseValidator.UpdateExpense(id, dto);
        }

        public Task<bool> UpdateTravelPlan(int id, UpdateTravelPlanDto dto, int userId)
        {
            return travelValidator.UpdateTravelPlan(id, dto, userId);
        }

        public Task<bool> UpdateTravelPlanAdmin(int id, UpdateTravelPlanDto dto)
        {
           return travelValidator.UpdateTravelPlanAdmin(id, dto);
        }

        public Task<bool> UpdateUser(int id, UpdateUserDto dto)
        {
            return userValidator.UpdateUser(id, dto);
        }


        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }
       
    }
}
