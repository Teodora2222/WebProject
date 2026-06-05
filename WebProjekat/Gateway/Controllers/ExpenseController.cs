using Contract.Dtos.Expense;
using Contract.Services;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/travel-plans/{travelPlanId}/expenses")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseGatewayService expenseService;

        public ExpenseController(IExpenseGatewayService expenseService)
        {
            this.expenseService = expenseService;
        }


        [HttpGet]
        public async Task<IActionResult> getAllExpenses(int travelPlanId)
        {
            try
            {
                var result = await expenseService.GetAllExpensesAsync(travelPlanId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> createExpense(int travelPlanId, [FromBody] CreateExpenseDto dto)
        {
            try
            {
                var result = await expenseService.CreateExpenseAsync(dto, travelPlanId);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteExpense(int id)
        {
            try
            {
                var result = await expenseService.DeleteExpenseAsync(id);
                if (!result)
                    return NotFound(new { success = false, message = "Expense not found" });

                return Ok(new { success = true, message = "Expense  deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateExpense(int id, [FromBody] UpdateExpenseDto dto)
        {
            try
            {
                var result = await expenseService.UpdateExpenseAsync(id, dto);
                if (!result)
                    return NotFound(new { success = false, message = "Expense not found" });
                return Ok(new { success = true, message = "Expense updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("summary")]
        public async Task<IActionResult> getExpenseSummary(int travelPlanId, [FromQuery] decimal budget)
        {
            try
            {
                var result = await expenseService.GetExpenseSummaryAsync(travelPlanId, budget);
                if (result == null)
                    return NotFound(new { success = false, message = "Expense not found" });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
