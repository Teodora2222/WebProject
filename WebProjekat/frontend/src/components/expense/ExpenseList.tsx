import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { IExpenseApi } from "../../api/expense/IExpenseApi";
import type { ExpenseSummaryDto } from "../../models/expense/ExpenseSummaryDto";
import { categoryStyles } from "../../helpers/auth/expenseStyles";

interface Props {
  expenseApi: IExpenseApi;
  budget: number;
}


export function ExpensesList({ expenseApi, budget }: Props) {
  const { id: travelPlanId } = useParams();
  const navigate = useNavigate();

  const [summary, setSummary] = useState<ExpenseSummaryDto | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchExpenses();
  }, []);

  const fetchExpenses = async () => {
    try {
      const data = await expenseApi.getSummary(Number(travelPlanId), budget);
      setSummary(data);
    } catch {
      toast.error("Failed to load expenses.");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: number, e: React.MouseEvent) => {
  e.stopPropagation();
  if (!confirm("Delete this expense?")) return;
  try {
    await expenseApi.deleteExpense(Number(travelPlanId), id);
    fetchExpenses();
    toast.success("Expense deleted.");
  } catch {
    toast.error("Failed to delete.");
  }
};
  

  const percent = summary
    ? (summary.totalExpenses / budget) * 100
    : 0;

    const progressWidth = Math.min(percent, 100);

const barColor =
  percent > 100
    ? "bg-red-500"
    : percent > 80
    ? "bg-yellow-400"
    : "bg-green-500";

  const isOverBudget = percent > 100;
  const isNearLimit = percent > 80;
  const remaining = summary
  ? budget - summary.totalExpenses
  : 0;

  return (
    <div className="bg-white/5 border border-white/10 rounded-3xl p-8">

      {/* HEADER */}
      <div className="flex justify-between items-center mb-8">
        <div>
          <h2 className="text-2xl font-bold text-white">Expenses</h2>
          <p className="text-white/40 text-sm">
            €{summary?.totalExpenses} of €{budget}
          </p>
        </div>

        <button
          onClick={() => navigate(`/trips/${travelPlanId}/expenses/new`)}
          className="px-5 py-2 rounded-lg text-sm font-medium
bg-green-500/20 text-green-300
border border-green-400/30
hover:bg-green-500/30 hover:border-green-300
transition" >
          Add Expense
        </button>
      </div>

      <div className="mb-8">
        <div className="flex justify-between text-sm text-white/40 mb-2">
          <span>Budget usage</span>
          <span className={remaining < 0 ? "text-red-400" : ""}>
  {remaining >= 0
    ? `€${remaining} left`
    : `Over by €${Math.abs(remaining)}`}
</span>
        </div>

        <div className="w-full h-2 bg-white/10 rounded-full overflow-hidden">
          <div
  className={`h-full transition-all duration-500 ${barColor}`}
  style={{ width: `${progressWidth}%` }}
/>
        </div>
      </div>

      <div className="flex flex-col gap-3 max-h-[450px] overflow-y-auto pr-2">
            {summary && (
  <>
    {isOverBudget && (
      <div className="mb-6 p-4 rounded-xl bg-red-500/10 border border-red-500/30 text-red-400 text-sm">
        ⚠️ You exceeded your budget by €{Math.abs(summary.remainingBudget)}
      </div>
    )}

    {!isOverBudget && isNearLimit && (
      <div className="mb-6 p-4 rounded-xl bg-yellow-500/10 border border-yellow-500/30 text-yellow-400 text-sm">
        ⚠️ You are close to your budget limit
      </div>
    )}
  </>
)}
        {summary?.expenses?.map((exp) => (
  <div
    key={exp.id}
    onClick={() => navigate(`/trips/${travelPlanId}/expenses/${exp.id}/edit`)}
    className="group flex justify-between items-center bg-white/5 border border-white/10 rounded-xl px-5 py-4 hover:border-green-400/30 transition cursor-pointer"
  >
    <div>
      <div className="flex items-center gap-2">
        <h3 className="text-white font-semibold">{exp.name}</h3>
        <span className={`text-xs px-2 py-1 rounded-lg border ${categoryStyles[exp.category]}`}>
          {exp.category}
        </span>
      </div>
      <p className="text-white/40 text-xs mt-1">
        {new Date(exp.date).toLocaleDateString()}
      </p>
    </div>

    <div className="flex items-center gap-6">
      <span className="text-white font-bold text-lg">€{exp.amount}</span>
      <button
        onClick={(e) => handleDelete(exp.id, e)}
        className="opacity-0 group-hover:opacity-100 p-2 bg-red-500/20 hover:bg-red-500 rounded-xl text-white transition-all flex-shrink-0"
       >
        <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
         </svg>
      </button>
    </div>
  </div>
))}

      </div>
    </div>
  );
}
