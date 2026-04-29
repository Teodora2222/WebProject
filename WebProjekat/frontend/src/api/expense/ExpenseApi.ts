import axios, { type AxiosInstance } from "axios";
import type { IExpenseApi } from "./IExpenseApi";
import type { ExpenseDto } from "../../models/expense/ExpenseDto";
import type { CreateExpenseDto } from "../../models/expense/CreateExpenseDto";
import type { UpdateExpenseDto } from "../../models/expense/UpdateExpenseDto";
import type { ExpenseSummaryDto } from "../../models/expense/ExpenseSummaryDto";

export class ExpenseApi implements IExpenseApi {

  private readonly axiosInstance: AxiosInstance;

  constructor(token: string) {
    this.axiosInstance = axios.create({
      baseURL: import.meta.env.VITE_EXPENSE_SERVICE_API,
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  async createExpense(travelPlanId: number, expense: CreateExpenseDto): Promise<ExpenseDto> {
    const result = await this.axiosInstance.post<ExpenseDto>(
      `/api/travel-plans/${travelPlanId}/expenses`,
      expense
    );
    return result.data;
  }

  async updateExpense(travelPlanId: number, id: number, expense: UpdateExpenseDto): Promise<boolean> {
    const result = await this.axiosInstance.put<boolean>(
      `/api/travel-plans/${travelPlanId}/expenses/${id}`,
      expense
    );
    return result.data;
  }

  async deleteExpense(travelPlanId: number, id: number): Promise<boolean> {
    const result = await this.axiosInstance.delete<boolean>(
      `/api/travel-plans/${travelPlanId}/expenses/${id}`
    );
    return result.data;
  }

  async getAllExpenses(travelPlanId: number): Promise<ExpenseDto[]> {
    const result = await this.axiosInstance.get<ExpenseDto[]>(
      `/api/travel-plans/${travelPlanId}/expenses`
    );
    return result.data;
  }

  async getSummary(travelPlanId: number, budget: number): Promise<ExpenseSummaryDto> {
    const result = await this.axiosInstance.get<ExpenseSummaryDto>(
      `/api/travel-plans/${travelPlanId}/expenses/summary?budget=${budget}`
    );
    return result.data;
  }
}