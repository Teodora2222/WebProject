import type { ExpenseCategory } from "../../enums/ExpenseCategory";

export interface CreateExpenseDto {
  name: string;
  category: ExpenseCategory; 
  amount: number;
  date: string;
  description?: string;
}