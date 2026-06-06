import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import type { TravelPlanDto } from "../../models/travel/TravelPlanDto";
import type { ITravelPlanApi } from "../../api/travel/ITravelPlanApi";
import { ConfirmModal } from "../modal/ConfirmModal";

const getSeasonImage = (dateStr: string) => {
  const month = new Date(dateStr).getMonth() + 1;
  if (month >= 6 && month <= 8) return "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?auto=format&fit=crop&w=800&q=80";
  if (month >= 3 && month <= 5) return "https://images.unsplash.com/photo-1490750967868-88aa4486c946?auto=format&fit=crop&w=800&q=80";
  if (month >= 9 && month <= 11) return "https://images.unsplash.com/photo-1503435980610-a51f3ddfee50?auto=format&fit=crop&w=800&q=80";
  return "https://images.unsplash.com/photo-1483921020237-2ff51e8e4b22?auto=format&fit=crop&w=800&q=80";
};

interface Props {
  travelPlanApi: ITravelPlanApi;
}

export function TravelPlansList({ travelPlanApi }: Props) {
  const [plans, setPlans] = useState<TravelPlanDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [deleteId, setDeleteId] = useState<number | null>(null);
  const navigate = useNavigate();

  useEffect(() => { fetchPlans(); }, []);

  const fetchPlans = async () => {
    try {
      const data = await travelPlanApi.getAllTravelPlans();
      setPlans(data);
    } catch {
      toast.error("Failed to load travel plans.");
    } finally {
      setLoading(false);
    }
  };

const handleDelete = (id: number) => {
  setDeleteId(id);
};

  const confirmDelete = async () => {
  if (!deleteId) return;

  try {
    await travelPlanApi.deleteTravelPlan(deleteId);

    setPlans(prev =>
      prev.filter(p => p.id !== deleteId)
    );

    toast.success("Plan deleted.");
  } catch {
    toast.error("Failed to delete.");
  } finally {
    setDeleteId(null);
  }
};

  const formatDate = (dateStr: string) =>
    new Date(dateStr).toLocaleDateString("en-GB", { day: "numeric", month: "short" });

  if (loading) return <div className="flex justify-center p-20"><div className="animate-spin rounded-full h-10 w-10 border-b-2 border-green-500"></div></div>;

  return (
    <div className="w-full max-w-6xl mx-auto px-4 md:px-6">
      
      <div className="flex flex-row items-end justify-between mb-8 pb-4 border-b border-white/10">
        <div>
          <h2 className="text-2xl font-bold text-white">My Trips</h2>
          <p className="text-white/50 text-sm">{plans.length} destinations found</p>
        </div>
        
        <button
          onClick={() => navigate("/trips/new")}
          className="bg-green-500 hover:bg-green-600 text-white px-5 py-2 rounded-xl font-bold transition-all transform hover:scale-105 shadow-lg shadow-green-500/20"
        >
          + New Trip
        </button>
      </div>

      {plans.length === 0 ? (
        <div className="text-center py-20 bg-white/5 rounded-3xl border border-dashed border-white/20 text-white/40">
          No trips planned yet.
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {plans.map((plan) => (
            <div
              key={plan.id}
              onClick={() => navigate(`/trips/${plan.id}`)}
              className="group bg-[#064e3b]/30 backdrop-blur-md border border-white/10 rounded-2xl overflow-hidden hover:border-green-500/50 transition-all duration-300 cursor-pointer"
            >
              <div className="relative h-48 overflow-hidden">
                <img
                  src={getSeasonImage(plan.startDate)}
                  className="w-full h-full object-cover transition duration-500 group-hover:scale-110"
                />
                <div className="absolute top-3 left-3 bg-black/50 backdrop-blur-md px-2 py-1 rounded-lg text-xs font-bold text-white">
                  €{plan.budget}
                </div>
                <button
                  onClick={(e) => { e.stopPropagation(); handleDelete(plan.id); }}
                  className="absolute top-3 right-3 p-2 bg-red-500/20 hover:bg-red-500 rounded-full text-white opacity-0 group-hover:opacity-100 transition-all"
                >
                  <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                  </svg>
                </button>
              </div>

              <div className="p-5">
                <h3 className="text-lg font-bold text-white mb-1 group-hover:text-green-400 transition-colors">
                  {plan.title}
                </h3>
                <p className="text-white/60 text-sm line-clamp-1 mb-4">
                  {plan.description || "Explore this trip..."}
                </p>
                <div className="flex justify-between items-center text-[11px] font-bold text-white/40 uppercase tracking-widest border-t border-white/5 pt-4">
                  <span>📅 {formatDate(plan.startDate)} - {formatDate(plan.endDate)}</span>
                  <span className="text-green-400">
                    {Math.ceil((new Date(plan.endDate).getTime() - new Date(plan.startDate).getTime()) / (1000 * 60 * 60 * 24))} Days
                  </span>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
      {deleteId && (
  <ConfirmModal
    title="Delete trip"
    message="All destinations, activities, expenses and checklist items will be removed."
    onConfirm={confirmDelete}
    onCancel={() => setDeleteId(null)}
  />
)}
    </div>
  );
}