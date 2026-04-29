import  { ExpenseCategory } from "../../enums/ExpenseCategory";

export interface ExpenseDto {
  id: number;
  travelPlanId: number;
  name: string;
  category: ExpenseCategory; 
  amount: number;
  date: string;
  description?: string;
}