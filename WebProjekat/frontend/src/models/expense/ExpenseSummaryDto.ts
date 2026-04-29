import type { ExpenseDto } from "./ExpenseDto";

export interface ExpenseSummaryDto {
  totalExpenses: number;
  remainingBudget: number;
  expenses: ExpenseDto[];
}