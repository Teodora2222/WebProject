import type { ExpenseCategory } from "../../enums/ExpenseCategory";

export interface UpdateExpenseDto {
  name?: string;
  category?: ExpenseCategory;
  amount?: number;
  date?: string;
  description?: string;
}