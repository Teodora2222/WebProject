import { ExpenseForm } from "../components/expense/ExpenseForm";
import { ExpenseApi } from "../api/expense/ExpenseApi";
import { useAuth } from "../hooks/useAuth";

export function EditExpensePage() {
  const { token } = useAuth();

  return (
    <ExpenseForm expenseApi={new ExpenseApi(token ?? "")} />
  );
}