import { useState } from "react";
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

type Tab = "destinations" | "activities" | "checklist";

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

  const tabs: { key: Tab; label: string; icon: string }[] = [
    { key: "destinations", label: "Destinations", icon: "📍" },
    { key: "activities", label: "Activities", icon: "🎯" },
    { key: "checklist", label: "Checklist", icon: "✅" },
  ];

return (
  <div className="min-h-screen flex flex-col bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] text-white">
    <NavBar />

    <div className="max-w-6xl mx-auto w-full px-6 pt-10 pb-6">

      <button
        onClick={() => navigate("/home")}
        className="mb-8 flex items-center gap-2 text-sm text-white/60 hover:text-white transition"
      >
        ← Back to trips
      </button>

      <div className="flex items-center justify-between mb-8">
        
        <div className="flex gap-2 bg-white/5 p-1 rounded-xl border border-white/10">
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
            <button
  onClick={() => navigate(`/trips/${id}/edit`)}
  className="px-12 py-2.5 text-sm rounded-xl 
  border border-yellow-400/50 
  text-yellow-300 
  bg-gradient-to-r from-yellow-400/10 to-orange-400/10 
  hover:from-yellow-400/20 hover:to-orange-400/20 
  hover:border-yellow-300 
  hover:shadow-[0_0_14px_rgba(250,204,21,0.6)]
  transition-all duration-200 
  font-semibold tracking-wide"
>
  Edit Trip
</button>
      </div>

      <div className="w-full">
        {activeTab === "destinations" && (
          <DestinationsList destinationApi={destinationApi} />
        )}
        {activeTab === "activities" && (
          <ActivitiesList activityApi={activityApi} />
        )}
        {activeTab === "checklist" && (
          <CheckList checkListApi={new CheckListItemApi(token ?? "")} />
        )}
      </div>

    </div>
  </div>
);
}