import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { ActivityDto } from "../../models/activity/ActivityDto";
import type { IActivityApi } from "../../api/activity/IActivityApi";
import { Status } from "../../enums/Status";

interface Props {
  activityApi: IActivityApi;
}

const statusColor: Record<Status, string> = {
  [Status.PLANNED]: "bg-blue-500/20 text-blue-400",
  [Status.RESERVED]: "bg-yellow-500/20 text-yellow-400",
  [Status.FINISHED]: "bg-green-500/20 text-green-400",
  [Status.CANCELLED]: "bg-red-500/20 text-red-400",
};

export function ActivitiesList({ activityApi }: Props) {
  const { id: travelPlanId } = useParams();
  const [activities, setActivities] = useState<ActivityDto[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    if (travelPlanId) fetchActivities();
  }, [travelPlanId]);

  const fetchActivities = async () => {
    try {
      const data = await activityApi.getAllActivities(Number(travelPlanId));
      setActivities(data);
    } catch {
      toast.error("Failed to load activities.");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (actId: number, e: React.MouseEvent) => {
    e.stopPropagation();
    if (!confirm("Delete this activity?")) return;
    try {
      await activityApi.deleteActivity(Number(travelPlanId), actId);
      setActivities((prev) => prev.filter((a) => a.id !== actId));
      toast.success("Activity deleted.");
    } catch {
      toast.error("Failed to delete.");
    }
  };

  const formatDate = (dateStr: string) =>
    new Date(dateStr).toLocaleDateString("en-GB", { day: "numeric", month: "short", year: "numeric" });

  const grouped = activities.reduce((acc, act) => {
  const key = act.date.substring(0, 10);

  if (!acc[key]) acc[key] = [];

  acc[key].push(act);

  acc[key].sort((a, b) => {
    if (!a.time) return 1;
    if (!b.time) return -1;

    return a.time.localeCompare(b.time);
  });

  return acc;
}, {} as Record<string, ActivityDto[]>);

  const sortedDates = Object.keys(grouped).sort();

  if (loading)
    return (
      <div className="flex justify-center p-10">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-green-500" />
      </div>
    );

  return (
    <div className="w-full">
      <div className="flex items-end justify-between mb-6 pb-4 border-b border-white/10">
        <div>
          <h2 className="text-xl font-bold text-white">Activities</h2>
          <p className="text-white/50 text-sm">{activities.length} planned</p>
        </div>
        <button
          onClick={() => navigate(`/trips/${travelPlanId}/activities/new`)}
          className="px-5 py-2 rounded-lg text-sm font-medium
bg-green-500/20 text-green-300
border border-green-400/30
hover:bg-green-500/30 hover:border-green-300
transition"  >
          + Add Activity
        </button>
      </div>

      {activities.length === 0 ? (
        <div className="text-center py-16 bg-white/5 rounded-3xl border border-dashed border-white/20 text-white/40">
          No activities planned yet.
        </div>
      ) : (
        <div className="flex flex-col gap-6">
          {sortedDates.map((date,index) => (
            <div key={date}>
             <div className="mb-4">
  <p className="text-green-400 text-sm font-semibold tracking-wide">
    DAY {index + 1}
  </p>

  <p className="text-white/40 text-xs font-bold uppercase tracking-widest mt-1">
    📅 {formatDate(date)}
  </p>
</div>
              <div className="flex flex-col gap-3">
                {grouped[date].map((act) => (
                  <div
                    key={act.id}
                    onClick={() => navigate(`/trips/${travelPlanId}/activities/${act.id}/edit`)}
                    className="group bg-[#064e3b]/30 backdrop-blur-md border border-white/10 rounded-2xl p-5 hover:border-green-500/50 hover:shadow-[0_0_25px_rgba(34,197,94,0.15)] transition-all duration-300 cursor-pointer flex items-start justify-between gap-4"
                  >
                    <div className="flex items-start gap-4 relative">
                      <div className="absolute left-5 top-10 bottom-0 w-px bg-white/10" />
                      <div className="w-10 h-10 rounded-xl bg-green-500/20 flex items-center justify-center text-lg flex-shrink-0">
                        🎯
                      </div>
                      <div>
                        <div className="flex items-center gap-2 flex-wrap">
                          <h3 className="text-white font-bold group-hover:text-green-400 transition-colors">
                            {act.name}
                          </h3>
                          {act.status && (
                            <span className={`text-xs px-2 py-0.5 rounded-lg font-medium ${statusColor[act.status]}`}>
                              {act.status}
                            </span>
                          )}
                        </div>
                        <div className="flex gap-4 mt-1 text-xs text-white/40 flex-wrap">
                          {act.time && <span>🕐 {act.time}</span>}
                          {act.location && <span>📍 {act.location}</span>}
                          {act.estimatedCost && <span>💰 €{act.estimatedCost}</span>}
                        </div>
                        {act.description && (
                          <p className="text-white/40 text-sm mt-2 line-clamp-2">{act.description}</p>
                        )}
                      </div>
                    </div>

                    <button
                      onClick={(e) => handleDelete(act.id, e)}
                      className="opacity-0 group-hover:opacity-100 p-2 bg-red-500/20 hover:bg-red-500 rounded-xl text-white transition-all flex-shrink-0"
                    >
                      <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                      </svg>
                    </button>
                  </div>
                ))}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}