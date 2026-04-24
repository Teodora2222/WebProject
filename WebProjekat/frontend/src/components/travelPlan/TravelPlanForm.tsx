import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { ITravelPlanApi } from "../../api/travel/ITravelPlanApi";

interface Props {
  travelPlanApi: ITravelPlanApi;
}

export function TravelPlanForm({ travelPlanApi }: Props) {
  const { id } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(id);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [budget, setBudget] = useState("");
  const [notes, setNotes] = useState("");
  const [loading, setLoading] = useState(false);
  const [fetching, setFetching] = useState(isEdit);

  useEffect(() => {
    if (isEdit && id) {
      travelPlanApi.getTravelPlan(Number(id))
        .then((plan) => {
          setTitle(plan.title);
          setDescription(plan.description || "");
          setStartDate(plan.startDate.substring(0, 10));
          setEndDate(plan.endDate.substring(0, 10));
          setBudget(String(plan.budget));
          setNotes(plan.note || "");
          setFetching(false);
        })
        .catch(() => {
          toast.error("Failed to load plan.");
          navigate("/home");
        });
    }
  }, [id]);

  const handleSubmit = async () => {
    if (!title || !startDate || !endDate || !budget) {
      toast.error("Please fill in all required fields.");
      return;
    }
    if (new Date(endDate) < new Date(startDate)) {
      toast.error("End date cannot be before start date.");
      return;
    }
    if (Number(budget) < 0) {
      toast.error("Budget cannot be negative.");
      return;
    }

    setLoading(true);
    try {
      const dto = {
        title,
        description: description || undefined,
        startDate,
        endDate,
        budget: Number(budget),
        notes: notes || undefined,
      };

      if (isEdit && id) {
        await travelPlanApi.updateTravelPlan(Number(id), dto);
        toast.success("Trip updated!");
      } else {
        await travelPlanApi.createTravelPlan(dto);
        toast.success("Trip created!");
      }
      navigate("/home");
    } catch {
      toast.error("Something went wrong.");
    } finally {
      setLoading(false);
    }
  };

  if (fetching) {
    return (
      <div className="min-h-screen bg-gray-900 flex items-center justify-center">
        <p className="text-gray-400">Loading...</p>
      </div>
    );
  }

return (
  <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] flex items-center justify-center px-4">

    <div className="w-full max-w-2xl animate-fade-in">

      <button
        onClick={() => navigate(-1)}
        className="mb-6 flex items-center gap-2 px-4 py-2 rounded-xl bg-white/10 border border-white/20 text-white hover:bg-white/20 transition-all duration-200"
      >
        <span className="text-lg">←</span>
        <span className="text-sm">Back</span>
      </button>

      <div className="bg-white/10 backdrop-blur-2xl border border-white/20 rounded-3xl p-8 shadow-[0_0_40px_rgba(0,0,0,0.4)] transition-all duration-300 hover:scale-[1.01] hover:shadow-[0_0_60px_rgba(0,0,0,0.6)]">

        <div className="mb-6">
          <h1 className="text-3xl font-bold text-white">
            {isEdit ? "Edit Trip" : "Create Trip"}
          </h1>
          <p className="text-gray-300 mt-1">
            {isEdit
              ? "Update your travel details"
              : "Plan your next adventure"}
          </p>
        </div>

        <div className="flex flex-col gap-5">

          <input
            placeholder="Trip name"
            className="w-full px-5 py-4 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 focus:shadow-[0_0_10px_rgba(16,185,129,0.6)] transition"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />

          <textarea
            placeholder="Description"
            className="w-full px-5 py-4 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 focus:shadow-[0_0_10px_rgba(16,185,129,0.6)] min-h-[90px] transition"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />

          <div className="grid grid-cols-2 gap-4">
            <input
              type="date"
              className="px-4 py-3 rounded-xl bg-white/20 text-white outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 transition"
              value={startDate}
              onChange={(e) => setStartDate(e.target.value)}
            />
            <input
              type="date"
              className="px-4 py-3 rounded-xl bg-white/20 text-white outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 transition"
              value={endDate}
              onChange={(e) => setEndDate(e.target.value)}
            />
          </div>

          <input
            type="number"
            placeholder="Budget (€)"
            className="px-5 py-4 rounded-xl bg-white/20 text-white outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 transition"
            value={budget}
            onChange={(e) => setBudget(e.target.value)}
          />

          <textarea
            placeholder="Notes"
            className="px-5 py-4 rounded-xl bg-white/20 text-white outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 min-h-[80px] transition"
            value={notes}
            onChange={(e) => setNotes(e.target.value)}
          />

          <div className="flex justify-end gap-3 mt-4">

            <button
              onClick={() => navigate(-1)}
              className="px-5 py-2 rounded-xl border border-white/20 text-gray-300 hover:bg-white/10 transition"
            >
              Cancel
            </button>

            <button
              onClick={handleSubmit}
              disabled={loading}
              className="px-6 py-3 rounded-xl bg-gradient-to-r from-emerald-400 via-green-500 to-teal-500 text-white font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 transition-all duration-200"
            >
              {loading
                ? "Saving..."
                : isEdit
                ? "Save Changes"
                : "Create Trip"}
            </button>

          </div>

        </div>
      </div>
    </div>
  </div>
);
}