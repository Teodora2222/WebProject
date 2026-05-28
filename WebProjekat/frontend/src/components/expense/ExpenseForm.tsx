import toast from "react-hot-toast";
import { ExpenseCategory } from "../../enums/ExpenseCategory";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { IExpenseApi } from "../../api/expense/IExpenseApi";
import type { ITravelPlanApi } from "../../api/travel/ITravelPlanApi";

interface Props {
  expenseApi: IExpenseApi;
  travelPlanApi: ITravelPlanApi;
}

export function ExpenseForm({ expenseApi,travelPlanApi }: Props) {
  const { id: travelPlanId, expId } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(expId);

  const [name, setName] = useState("");
  const [amount, setAmount] = useState("");
  const [category, setCategory] = useState<ExpenseCategory>("Food");
  const [date, setDate] = useState("");
  const [description, setDescription] = useState("");
  const [loading, setLoading] = useState(false);

  const [tripStart, setTripStart] = useState("");
  const [tripEnd, setTripEnd] = useState("");

  const inputClass =
    "w-full px-5 py-4 rounded-xl bg-white/10 text-white placeholder-white/40 outline-none focus:ring-2 focus:ring-green-400";

    useEffect(() => {
  if (travelPlanId) {
    travelPlanApi.getTravelPlan(Number(travelPlanId)).then((trip) => {
      setTripStart(trip.startDate.substring(0, 10));
      setTripEnd(trip.endDate.substring(0, 10));
    });
  }
}, [travelPlanId]);

    useEffect(() => {
  if (isEdit && expId && travelPlanId) {
    expenseApi.getAllExpenses(Number(travelPlanId)).then((expenses) => {
      const exp = expenses.find((e) => e.id === Number(expId));
      if (exp) {
        setName(exp.name);
        setAmount(String(exp.amount));
        setCategory(exp.category);
        setDate(exp.date.substring(0, 10));
        setDescription(exp.description || "");
      }
    });
  }
}, [expId]);
  const handleSubmit = async () => {
    if (!name || !amount || !date) {
      toast.error("Fill required fields");
      return;
    }

    if (Number(amount) <= 0) {
  toast.error("Amount must be greater than 0.");
  return;
}

if (date < tripStart || date > tripEnd) {
  toast.error(`Expense date must be between ${tripStart} and ${tripEnd}.`);
  return;
}

    try {
      setLoading(true);

      const dto = {
  name,
  amount: Number(amount),
  category,
  date: new Date(date).toISOString(),
  description: description || undefined,
};

      if (isEdit && expId) {
        await expenseApi.updateExpense(Number(travelPlanId), Number(expId), dto);
        toast.success("Updated");
      } else {
        console.log("DTO:", dto);
        await expenseApi.createExpense(Number(travelPlanId), dto);
        toast.success("Added");
      }

      navigate(`/trips/${travelPlanId}`, { state: { tab: "budget" } });
    } catch {
      toast.error("Error");
    } finally {
      setLoading(false);
    }
  };


  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617]">

      <div className="w-full max-w-xl bg-white/10 rounded-3xl p-8 border border-white/10">
         <button
          onClick={() => navigate(-1)}
          className="mb-6 flex items-center gap-2 px-4 py-2 rounded-xl bg-white/10 border border-white/20 text-white hover:bg-white/20 transition-all duration-200"
        >
          <span className="text-lg">←</span>
          <span className="text-sm">Back</span>
        </button>
        
        <h1 className="text-2xl font-bold text-white mb-6">
          {isEdit ? "Edit Expense" : "Add Expense"}
        </h1>

        <div className="flex flex-col gap-4">

          <input
            placeholder="Name"
            className={inputClass}
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          <input
            type="number"
            placeholder="Amount (€)"
            className={inputClass}
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
          />

          <select
  value={category}
  onChange={(e) => setCategory(e.target.value as ExpenseCategory)}
  className="w-full px-5 py-4 rounded-xl text-white outline-none focus:ring-2 focus:ring-green-400 transition cursor-pointer"
  style={{ background: "rgba(255,255,255,0.1)" }}
>
  {Object.values(ExpenseCategory).map((c) => (
    <option key={c} value={c} style={{ background: "#064e3b", color: "white" }}>
      {c}
    </option>
  ))}
</select>

          <input
            type="date"
            className={inputClass}
            value={date}
            min={tripStart}
            max={tripEnd}
            onChange={(e) => setDate(e.target.value)}
          />

          <textarea
            placeholder="Description"
            className={`${inputClass} min-h-[80px]`}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />

          <button
            onClick={handleSubmit}
            className="mt-4 px-6 py-3 rounded-xl bg-green-500 hover:bg-green-600 text-white font-semibold transition"
          >
            {loading ? "Saving..." : "Save"}
          </button>

        </div>
      </div>
    </div>
  );
}