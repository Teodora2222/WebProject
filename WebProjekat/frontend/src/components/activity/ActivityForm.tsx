import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { IActivityApi } from "../../api/activity/IActivityApi";
import { Status } from "../../enums/Status";
import type { ITravelPlanApi } from "../../api/travel/ITravelPlanApi";

interface Props {
  activityApi: IActivityApi;
  travelPlanApi: ITravelPlanApi;
}

export function ActivityForm({ activityApi ,travelPlanApi}: Props) {
  const { id: travelPlanId, actId } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(actId);

  const [name, setName] = useState("");
  const [date, setDate] = useState("");
  const [time, setTime] = useState("");
  const [location, setLocation] = useState("");
  const [description, setDescription] = useState("");
  const [estimatedCost, setEstimatedCost] = useState("");
  const [status, setStatus] = useState<Status>(Status.PLANNED);
  const [loading, setLoading] = useState(false);
  const [fetching, setFetching] = useState(isEdit);
  const [tripStart, setTripStart] = useState("");
  const [tripEnd, setTripEnd] = useState("");

  useEffect(() => {
  if (travelPlanId) {
    travelPlanApi.getTravelPlan(Number(travelPlanId)).then((trip) => {
      setTripStart(trip.startDate.substring(0, 10));
      setTripEnd(trip.endDate.substring(0, 10));
    });
  }
}, [travelPlanId]);

  useEffect(() => {
    if (isEdit && actId && travelPlanId) {
      activityApi.getActivity(Number(travelPlanId), Number(actId))
        .then((act) => {
          setName(act.name);
          setDate(act.date.substring(0, 10));
          setTime(act.time || "");
          setLocation(act.location || "");
          setDescription(act.description || "");
          setEstimatedCost(act.estimatedCost ? String(act.estimatedCost) : "");
          setStatus(act.status || Status.PLANNED);
          setFetching(false);
        })
        .catch(() => {
          toast.error("Failed to load activity.");
          navigate(`/trips/${travelPlanId}`, { state: { tab: "activities" } });
        });
    }
  }, [actId]);

  const handleSubmit = async () => {
    if (!name || !date) {
      toast.error("Name and date are required.");
      return;
    }
    if (date < tripStart || date > tripEnd) {
      toast.error(`Activity date must be between ${tripStart} and ${tripEnd}.`);
      return;
    }
    if (estimatedCost && Number(estimatedCost) < 0) {
      toast.error("Cost cannot be negative.");
      return;
    }

    setLoading(true);
    try {
      const dto = {
        name,
        date,
        time: time || undefined,
        location: location || undefined,
        description: description || undefined,
        estimatedCost: estimatedCost ? Number(estimatedCost) : undefined,
        status,
      };

      if (isEdit && actId) {
        await activityApi.updateActivity(Number(travelPlanId), Number(actId), dto);
        toast.success("Activity updated!");
      } else {
        await activityApi.createActivity(Number(travelPlanId), dto);
        toast.success("Activity added!");
      }
      navigate(`/trips/${travelPlanId}`, { state: { tab: "activities" } });
    } catch {
      toast.error("Something went wrong.");
    } finally {
      setLoading(false);
    }
  };

  if (fetching)
    return (
      <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] flex items-center justify-center">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-green-500" />
      </div>
    );

  const inputClass = "w-full px-5 py-4 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none hover:bg-white/30 focus:ring-2 focus:ring-emerald-400 transition";

  return (
    <div className="min-h-screen bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] flex items-center justify-center px-4">
      <div className="w-full max-w-2xl">

        <button
          onClick={() => navigate(`/trips/${travelPlanId}`, { state: { tab: "activities" } })}
          className="mb-6 flex items-center gap-2 px-4 py-2 rounded-xl bg-white/10 border border-white/20 text-white hover:bg-white/20 transition-all duration-200"
        >
          <span className="text-lg">←</span>
          <span className="text-sm">Back</span>
        </button>

        <div className="bg-white/10 backdrop-blur-2xl border border-white/20 rounded-3xl p-8 shadow-[0_0_40px_rgba(0,0,0,0.4)]">

          <div className="mb-6">
            <h1 className="text-3xl font-bold text-white">
              {isEdit ? "Edit Activity" : "Add Activity"}
            </h1>
            <p className="text-gray-300 mt-1">
              {isEdit ? "Update activity details" : "Plan what you'll do"}
            </p>
          </div>

          <div className="flex flex-col gap-5">
            <input
              placeholder="Activity name *"
              className={inputClass}
              value={name}
              onChange={(e) => setName(e.target.value)}
            />

            <div className="grid grid-cols-2 gap-4">
              <div className="flex flex-col gap-1">
                <label className="text-white/50 text-xs pl-1">Date *</label>
                <input
                  type="date"
                  min={tripStart}   
                  max={tripEnd} 
                  className={inputClass}
                  value={date}
                  onChange={(e) => setDate(e.target.value)}
                />
              </div>
              <div className="flex flex-col gap-1">
                <label className="text-white/50 text-xs pl-1">Time</label>
                <input
                  type="time"
                  className={inputClass}
                  value={time}
                  onChange={(e) => setTime(e.target.value)}
                />
              </div>
            </div>

            <input
              placeholder="Location"
              className={inputClass}
              value={location}
              onChange={(e) => setLocation(e.target.value)}
            />

            <textarea
              placeholder="Description"
              className={`${inputClass} min-h-[80px] resize-y`}
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />

            <div className="grid grid-cols-2 gap-4">
              <div className="flex flex-col gap-1">
                <label className="text-white/50 text-xs pl-1">Estimated cost (€)</label>
                <input
                  type="number"
                  min="0"
                  placeholder="0"
                  className={inputClass}
                  value={estimatedCost}
                  onChange={(e) => setEstimatedCost(e.target.value)}
                />
              </div>
              <div className="flex flex-col gap-1">
                <label className="text-white/50 text-xs pl-1">Status</label>
             <select
  className="w-full px-5 py-4 rounded-xl text-white outline-none focus:ring-2 focus:ring-emerald-400 transition cursor-pointer"
  style={{ background: "rgba(255,255,255,0.2)" }}
  value={status}
  onChange={(e) => setStatus(e.target.value as Status)}
>
  <option value={Status.PLANNED} style={{ background: "#064e3b", color: "white" }}>Planned</option>
  <option value={Status.RESERVED} style={{ background: "#064e3b", color: "white" }}>Reserved</option>
  <option value={Status.FINISHED} style={{ background: "#064e3b", color: "white" }}>Finished</option>
  <option value={Status.CANCELLED} style={{ background: "#064e3b", color: "white" }}>Cancelled</option>
</select>
              </div>
            </div>

            <div className="flex justify-end gap-3 mt-2">
              <button
                onClick={() => navigate(`/trips/${travelPlanId}`, { state: { tab: "activities" } })}
                className="px-5 py-2 rounded-xl border border-white/20 text-gray-300 hover:bg-white/10 transition"
              >
                Cancel
              </button>
              <button
                onClick={handleSubmit}
                disabled={loading}
                className="px-6 py-3 rounded-xl bg-gradient-to-r from-emerald-400 via-green-500 to-teal-500 text-white font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 transition-all duration-200 disabled:opacity-60"
              >
                {loading ? "Saving..." : isEdit ? "Save Changes" : "Add Activity"}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}