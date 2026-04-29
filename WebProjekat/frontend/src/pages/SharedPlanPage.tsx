import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";

export function SharedPlanPage() {
  const { token } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    if (token) {
      axios
        .get(`${import.meta.env.VITE_TRIP_SERVICE_API}/api/shared/${token}`)
        .then((res) => setData(res.data))
        .catch(() => setError(true))
        .finally(() => setLoading(false));
    }
  }, [token]);

  if (loading)
    return (
      <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] flex items-center justify-center">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-green-500" />
      </div>
    );

  if (error || !data)
    return (
      <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] flex items-center justify-center">
        <div className="text-center">
          <p className="text-5xl mb-4">🔗</p>
          <p className="text-white font-bold text-xl">Invalid or expired link</p>
          <p className="text-white/40 text-sm mt-2">This share link is not valid.</p>
        </div>
      </div>
    );

  const isReadOnly = data.permission === "VIEW";
  const plan = data.plan;

  const formatDate = (dateStr: string) =>
    new Date(dateStr).toLocaleDateString("en-GB", {
      day: "numeric", month: "short", year: "numeric"
    });

  const getDuration = () => {
    const days = Math.ceil(
      (new Date(plan.endDate).getTime() - new Date(plan.startDate).getTime()) / (1000 * 60 * 60 * 24)
    );
    return `${days} day${days !== 1 ? "s" : ""}`;
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] text-white">

      {/* Header */}
      <div className="border-b border-white/10 px-6 py-4 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <span className="text-2xl">✈️</span>
          <span className="font-bold text-white">Travel Planner</span>
        </div>
        <span className={`text-xs px-3 py-1.5 rounded-xl border font-semibold ${
          isReadOnly
            ? "border-yellow-400/30 text-yellow-300 bg-yellow-500/10"
            : "border-blue-400/30 text-blue-300 bg-blue-500/10"
        }`}>
          {isReadOnly ? "👁️ View only" : "✏️ Edit access"}
        </span>
      </div>

      <div className="max-w-3xl mx-auto px-6 py-10 space-y-6">

        {/* Hero */}
        <div className="bg-white/5 border border-white/10 rounded-3xl p-8">
          <div className="flex items-start justify-between gap-4">
            <div>
              <h1 className="text-4xl font-bold text-white mb-2">{plan.title}</h1>
              {plan.description && (
                <p className="text-white/50 text-base">{plan.description}</p>
              )}
            </div>
            <div className="w-14 h-14 rounded-2xl bg-green-500/20 flex items-center justify-center text-2xl flex-shrink-0">
              {plan.title?.charAt(0).toUpperCase()}
            </div>
          </div>

          {/* Stats */}
          <div className="grid grid-cols-3 gap-4 mt-8">
            <div className="bg-white/5 rounded-2xl p-4 border border-white/5">
              <p className="text-white/40 text-xs uppercase tracking-wide mb-1">Start</p>
              <p className="text-white font-semibold">{formatDate(plan.startDate)}</p>
            </div>
            <div className="bg-white/5 rounded-2xl p-4 border border-white/5">
              <p className="text-white/40 text-xs uppercase tracking-wide mb-1">End</p>
              <p className="text-white font-semibold">{formatDate(plan.endDate)}</p>
            </div>
            <div className="bg-white/5 rounded-2xl p-4 border border-white/5">
              <p className="text-white/40 text-xs uppercase tracking-wide mb-1">Duration</p>
              <p className="text-green-400 font-semibold">{getDuration()}</p>
            </div>
          </div>
        </div>

        {/* Budget & Notes */}
        <div className="grid grid-cols-2 gap-4">
          <div className="bg-white/5 border border-white/10 rounded-2xl p-6">
            <p className="text-white/40 text-xs uppercase tracking-wide mb-2">Budget</p>
            <p className="text-green-400 font-bold text-3xl">€{plan.budget}</p>
          </div>
          <div className="bg-white/5 border border-white/10 rounded-2xl p-6">
            <p className="text-white/40 text-xs uppercase tracking-wide mb-2">Notes</p>
            <p className="text-white/70 text-sm">{plan.notes || "—"}</p>
          </div>
        </div>

        {/* Edit access CTA */}
        {!isReadOnly && (
          <div className="bg-blue-500/10 border border-blue-400/20 rounded-2xl p-6 flex items-center justify-between">
            <div>
              <p className="text-blue-300 font-semibold">You have edit access</p>
              <p className="text-white/40 text-sm mt-1">Log in to make changes to this trip</p>
            </div>
            <button
              onClick={() => navigate("/")}
              className="px-5 py-2.5 rounded-xl bg-blue-500 hover:bg-blue-600 text-white font-semibold text-sm transition"
            >
              Log in to edit
            </button>
          </div>
        )}

        {/* View only notice */}
        {isReadOnly && (
          <p className="text-white/20 text-sm text-center py-4">
            👁️ You have view-only access to this trip plan.
          </p>
        )}
      </div>
    </div>
  );
}