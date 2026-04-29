import { useEffect, useState } from "react";
import { useNavigate,useParams } from "react-router-dom";
import { DestinationsList } from "../components/destination/DestinationList";
import { ActivitiesList } from "../components/activity/ActivityList";
import { DestinationApi } from "../api/destination/DestinationApi";
import { ActivityApi } from "../api/activity/ActivityApi";
import { NavBar } from "../components/menu/NavBar";
import { useAuth } from "../hooks/useAuth";
import { CheckList } from "../components/checkList/CheckList";
import { CheckListItemApi } from "../api/checkListItem/CheckListItemApi";
import { useLocation } from "react-router-dom";
import { ExpensesList } from "../components/expense/ExpenseList";
import { ExpenseApi } from "../api/expense/ExpenseApi";
import type { TravelPlanDto } from "../models/travel/TravelPlanDto";
import { TravelPlanApi } from "../api/travel/TravelPlanApi";
import { ShareApi } from "../api/share/ShareApi";
import { ShareModal } from "../components/share/ShareModal";

type Tab = "destinations" | "activities" | "checklist" | "budget";

export function TripDetailPage() {
  const { token } = useAuth();
  const { id } = useParams();
  const navigate = useNavigate();
  const location = useLocation();
  const [activeTab, setActiveTab] = useState<Tab>(
    location.state?.tab ?? "destinations"
  );

  const destinationApi = new DestinationApi(token ?? "");
  const activityApi = new ActivityApi(token ?? "");
  const [trip, setTrip] = useState<TravelPlanDto | null>(null);
  const travelPlanApi = new TravelPlanApi(token ?? "");
  const [showShare, setShowShare] = useState(false);
  const shareApi = new ShareApi(token ?? "");

useEffect(() => {
  if (id) {
    travelPlanApi.getTravelPlan(Number(id)).then(setTrip);
  }
}, [id]);

  const tabs: { key: Tab; label: string; icon: string }[] = [
    { key: "destinations", label: "Destinations", icon: "📍" },
    { key: "activities", label: "Activities", icon: "🎯" },
    { key: "checklist", label: "Checklist", icon: "✅" },
    { key: "budget", label: "Budget" , icon : ""},
  ];

return (
  <div className="h-screen flex  hover:shadow-[0_0_30px_rgba(34,197,94,0.1)]
transition flex-col bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] text-white">
    <NavBar />

     <div className="flex-1 overflow-y-auto">
    <div className="max-w-6xl mx-auto w-full px-6 pt-8 pb-10 space-y-6">

      <button
        onClick={() => navigate("/home")}
        className="flex items-center gap-2 text-sm text-white/50 hover:text-white transition"
      >
        ← Back to trips
      </button>

    {trip && (
  <div className="bg-white/5 border border-white/10 rounded-2xl p-6">

    <div className="flex items-center justify-between mb-4">
      <h2 className="text-sm text-white/50 uppercase tracking-wide">
        Trip Information
      </h2>

      <button
        onClick={() => navigate(`/trips/${id}/edit`)}
        className="px-6 py-2 rounded-lg text-sm font-medium bg-gradient-to-r from-yellow-400/20 to-orange-400/20
            text-yellow-200 border border-yellow-400/30 hover:from-yellow-400/30 hover:to-orange-400/30
            hover:border-yellow-300 hover:shadow-[0_0_12px_rgba(250,204,21,0.4)] transition-all duration-200"
          >
          Edit
      </button>
      <button onClick={() => setShowShare(true)} className="px-4 py-2 rounded-lg border border-white/20 text-white/70 hover:bg-white/10 text-sm transition">
          🔗 Share
      </button>
    </div>

    <div className="grid grid-cols-2 md:grid-cols-4 gap-6">

      <div>
        <p className="text-white/40 text-xs tracking-wide uppercase">Title</p>
        <p className="text-white font-semibold text-[15px]">{trip.title}</p>
      </div>

      <div>
        <p className="text-white/40 text-xs">Dates</p>
        <p className="text-white">
          {trip.startDate.substring(0, 10)} → {trip.endDate.substring(0, 10)}
        </p>
      </div>

      <div>
        <p className="text-white/40 text-xs">Budget</p>
        <p className="text-green-400 font-medium">€{trip.budget}</p>
      </div>

      <div>
        <p className="text-white/40 text-xs">Description</p>
        <p className="text-white/80 text-sm">
          {trip.description || "-"}
        </p>
      </div>

    </div>

  </div>
)}

      <div className="flex items-center justify-between">
        <div className="flex gap-2 bg-black/30 backdrop-blur-xl p-1 rounded-xl border border-white/10">
          {tabs.map((tab) => (
            <button
              key={tab.key}
              onClick={() => setActiveTab(tab.key)}
              className={`px-5 py-2 rounded-lg text-sm font-medium transition-all ${
                activeTab === tab.key
                  ? "bg-green-500/20 text-green-300 border border-green-400/30"
                  : "text-white/50 hover:text-white"
              }`}
            >
              {tab.label}
            </button>
          ))}
        </div>
      </div>

      <div className="pt-2">
        {activeTab === "destinations" && (
          <DestinationsList destinationApi={destinationApi} />
        )}

        {activeTab === "activities" && (
          <ActivitiesList activityApi={activityApi} />
        )}

        {activeTab === "checklist" && (
          <CheckList checkListApi={new CheckListItemApi(token ?? "")} />
        )}

        {activeTab === "budget" && (
          <ExpensesList expenseApi={new ExpenseApi(token ?? "")} budget={trip?.budget ?? 0} />
        )}

        {showShare && (
          <ShareModal travelPlanId={Number(id)} shareApi={shareApi} onClose={() => setShowShare(false)} />
        )}
        </div>
      </div>
    </div>
  </div>
);
}