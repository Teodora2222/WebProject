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
  "w-full px-5 py-4 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 transition";
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
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] px-4">

  <div className="w-full max-w-xl">

    <button
      onClick={() =>
        navigate(`/trips/${travelPlanId}`, {
          state: { tab: "budget" }
        })
      }
      className="mb-6 flex items-center gap-2 px-4 py-2 rounded-xl bg-white/10 border border-white/20 text-white hover:bg-white/20 transition-all duration-200"
     >
      <span>←</span>
      <span>Back</span>
    </button>

    <div className="
  bg-white/10
  backdrop-blur-2xl
  border border-white/20
  rounded-3xl
  p-8
  shadow-[0_0_40px_rgba(0,0,0,0.4)]
">
        
        <h1 className="text-3xl font-bold text-white mb-6">
          {isEdit ? "Edit Expense" : "Add Expense"}
        </h1>
         <p className="text-gray-300 mt-1">
    {isEdit
      ? "Update expense details"
      : "Track your trip expenses"}
  </p>

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

         <div className="flex justify-end gap-3 mt-4">
  <button
    onClick={() =>
      navigate(`/trips/${travelPlanId}`, {
        state: { tab: "budget" }
      })
    }
    className="
      px-5 py-3
      rounded-xl
      border border-white/20
      text-white/70
      hover:bg-white/10
      transition
    "
  >
    Cancel
  </button>

  <button
  onClick={handleSubmit}
  disabled={loading}
  className="
    px-6 py-3
    rounded-xl
    bg-gradient-to-r
    from-emerald-400
    via-green-500
    to-teal-500
    text-white
    font-semibold
    shadow-lg
    hover:shadow-xl
    hover:scale-105
    active:scale-95
    transition-all
    duration-200
    disabled:opacity-60
  "
>
  {loading ? "Saving..." : isEdit ? "Save Changes" : "Add Expense"}
</button>
</div>

        </div>
      </div>
    </div>
    </div>
  );
}