import type { ExpenseDto } from "./ExpenseDto";

export interface ExpenseSummaryDto {
  totalExpenses: number;
  remainingBudget: number;
  plannedActivitiesCost: number;
  expenses: ExpenseDto[];
}