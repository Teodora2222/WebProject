import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { IExpenseApi } from "../../api/expense/IExpenseApi";
import type { ExpenseSummaryDto } from "../../models/expense/ExpenseSummaryDto";
import { categoryStyles } from "../../helpers/auth/expenseStyles";
import { ConfirmModal } from "../modal/ConfirmModal";

interface Props {
  expenseApi: IExpenseApi;
  budget: number;
}


export function ExpensesList({ expenseApi, budget }: Props) {
  const { id: travelPlanId } = useParams();
  const navigate = useNavigate();

  const [summary, setSummary] = useState<ExpenseSummaryDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [deleteId, setDeleteId] = useState<number | null>(null);

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

  const handleDelete = (destId: number, e: React.MouseEvent) => {
  e.stopPropagation();
  setDeleteId(destId);
};

    const confirmDelete = async () => {
  if (!deleteId) return;

  try {
    await expenseApi.deleteExpense(
      Number(travelPlanId),
      deleteId
    );

    fetchExpenses();

    toast.success("Expense deleted.");
  } catch {
    toast.error("Failed to delete.");
  } finally {
    setDeleteId(null);
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
  <div className="w-full">
    <div className="flex items-end justify-between mb-6 pb-4 border-b border-white/10">
      <div>
        <h2 className="text-xl font-bold text-white">Expenses</h2>
        <p className="text-white/50 text-sm">
          €{summary?.totalExpenses ?? 0} of €{budget}
        </p>
      </div>

      <button
        onClick={() => navigate(`/trips/${travelPlanId}/expenses/new`)}
        className="
          px-5 py-2 rounded-lg text-sm font-medium
          bg-green-500/20 text-green-300
          border border-green-400/30
          hover:bg-green-500/30 hover:border-green-300
          transition
        "
      >
        + Add Expense
      </button>
    </div>

    {summary && (
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
        <div className="bg-white/5 border border-white/10 rounded-xl p-4">
          <p className="text-white/40 text-xs uppercase">
            Budget
          </p>

          <p className="text-white text-2xl font-bold">
            €{budget}
          </p>
        </div>

        <div className="bg-blue-500/10 border border-blue-500/20 rounded-xl p-4">
          <p className="text-blue-300/70 text-xs uppercase">
            Planned Activities
          </p>

          <p className="text-blue-300 text-2xl font-bold">
            €{summary.plannedActivitiesCost}
          </p>
        </div>
      </div>
    )}

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

    {summary && (
      <>
        {isOverBudget && (
          <div className="mb-6 p-4 rounded-xl bg-red-500/10 border border-red-500/30 text-red-400 text-sm">
            ⚠️ You exceeded your budget by €
            {Math.abs(summary.remainingBudget)}
          </div>
        )}

        {!isOverBudget && isNearLimit && (
          <div className="mb-6 p-4 rounded-xl bg-yellow-500/10 border border-yellow-500/30 text-yellow-400 text-sm">
            ⚠️ You are close to your budget limit
          </div>
        )}
      </>
    )}

    {summary?.expenses?.length === 0 ? (
      <div className="text-center py-16 bg-white/5 rounded-3xl border border-dashed border-white/20 text-white/40">
        No expenses added yet.
      </div>
    ) : (
      <div className="flex flex-col gap-4">
        {summary?.expenses?.map((exp) => (
          <div
            key={exp.id}
            onClick={() =>
              navigate(
                `/trips/${travelPlanId}/expenses/${exp.id}/edit`
              )
            }
            className="
              group
              bg-[#064e3b]/30
              backdrop-blur-md
              border border-white/10
              rounded-2xl
              p-5
              hover:border-green-500/50
              transition-all duration-300
              cursor-pointer
              flex items-start justify-between gap-4
            "
          >
            <div className="flex items-start gap-4">
              <div className="
                w-10 h-10
                rounded-xl
                bg-green-500/20
                flex items-center justify-center
                text-green-400
                text-lg
                flex-shrink-0
              ">
                💰
              </div>

              <div>
                <div className="flex items-center gap-2">
                  <h3 className="text-white font-bold">
                    {exp.name}
                  </h3>

                  <span
                    className={`
                      text-xs px-2 py-1 rounded-lg border
                      ${categoryStyles[exp.category]}
                    `}
                  >
                    {exp.category}
                  </span>
                </div>

                <p className="text-white/40 text-sm mt-1">
                  📅 {new Date(exp.date).toLocaleDateString()}
                </p>

                {exp.description && (
                  <p className="text-white/40 text-sm mt-2 line-clamp-2">
                    {exp.description}
                  </p>
                )}
              </div>
            </div>

            <div className="flex items-center gap-4">
              <span className="text-green-400 font-bold text-lg">
                €{exp.amount}
              </span>

              <button
                onClick={(e) => handleDelete(exp.id, e)}
                className="
                  opacity-0 group-hover:opacity-100
                  p-2
                  bg-red-500/20
                  hover:bg-red-500
                  rounded-xl
                  text-white
                  transition-all
                  flex-shrink-0
                "
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="h-4 w-4"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                  />
                </svg>
              </button>
            </div>
          </div>
        ))}
      </div>
    )}

    {deleteId && (
      <ConfirmModal
        title="Delete expense"
        message="This action cannot be undone."
        onConfirm={confirmDelete}
        onCancel={() => setDeleteId(null)}
      />
    )}
  </div>
);
}