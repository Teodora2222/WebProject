import type { ExpenseDto } from "../../models/expense/ExpenseDto";
import type { CreateExpenseDto } from "../../models/expense/CreateExpenseDto";
import type { UpdateExpenseDto } from "../../models/expense/UpdateExpenseDto";
import type { ExpenseSummaryDto } from "../../models/expense/ExpenseSummaryDto";

export interface IExpenseApi {
  createExpense(travelPlanId: number, expense: CreateExpenseDto): Promise<ExpenseDto>;

  updateExpense(travelPlanId: number, id: number, expense: UpdateExpenseDto): Promise<boolean>;

  deleteExpense(travelPlanId: number, id: number): Promise<boolean>;

  getAllExpenses(travelPlanId: number): Promise<ExpenseDto[]>;

  getSummary(travelPlanId: number, budget: number): Promise<ExpenseSummaryDto>;
}