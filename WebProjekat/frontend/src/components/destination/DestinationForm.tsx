import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { IDestinationApi } from "../../api/destination/IDestinationApi";
import type { ITravelPlanApi } from "../../api/travel/ITravelPlanApi";

interface Props {
  destinationApi: IDestinationApi;
  travelPlanApi: ITravelPlanApi;
}

export function DestinationForm({ destinationApi ,travelPlanApi}: Props) {
  const { id: travelPlanId, destId } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(destId);

  const [name, setName] = useState("");
  const [location, setLocation] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [description, setDescription] = useState("");
  const [note, setNote] = useState("");
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
    if (isEdit && destId && travelPlanId) {
      destinationApi.getDestination(Number(travelPlanId), Number(destId))
        .then((dest) => {
          setName(dest.name);
          setLocation(dest.location || "");
          setStartDate(dest.startDate ? dest.startDate.substring(0, 10) : "");
          setEndDate(dest.endDate ? dest.endDate.substring(0, 10) : "");
          setDescription(dest.description || "");
          setNote(dest.note || "");
          setFetching(false);
        })
        .catch(() => {
          toast.error("Failed to load destination.");
          navigate(-1);
        });
    }
  }, [destId]);

  const handleSubmit = async () => {
    if (!name) {
      toast.error("Destination name is required.");
      return;
    }
    if (startDate && endDate && new Date(endDate) < new Date(startDate)) {
      toast.error("Departure cannot be before arrival.");
      return;
    }
    if (startDate && (startDate < tripStart || startDate > tripEnd)) {
  toast.error("Arrival date must be inside trip dates.");
  return;
}

if (endDate && (endDate < tripStart || endDate > tripEnd)) {
  toast.error("Departure date must be inside trip dates.");
  return;
}

    setLoading(true);
    try {
      const dto = {
        name,
        location: location || undefined,
        startDate: startDate || undefined,
        endDate: endDate || undefined,
        description: description || undefined,
        note: note || undefined,
      };

      if (isEdit && destId) {
        await destinationApi.updateDestination(Number(travelPlanId), Number(destId), dto);
        toast.success("Destination updated!");
      } else {
        await destinationApi.createDestination(Number(travelPlanId), dto);
        toast.success("Destination added!");
      }
      navigate(-1);
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
          onClick={() => navigate(-1)}
          className="mb-6 flex items-center gap-2 px-4 py-2 rounded-xl bg-white/10 border border-white/20 text-white hover:bg-white/20 transition-all duration-200"
        >
          <span className="text-lg">←</span>
          <span className="text-sm">Back</span>
        </button>

        <div className="bg-white/10 backdrop-blur-2xl border border-white/20 rounded-3xl p-8 shadow-[0_0_40px_rgba(0,0,0,0.4)]">

          <div className="mb-6">
            <h1 className="text-3xl font-bold text-white">
              {isEdit ? "Edit Destination" : "Add Destination"}
            </h1>
            <p className="text-gray-300 mt-1">
              {isEdit ? "Update destination details" : "Add a place to visit"}
            </p>
          </div>

          <div className="flex flex-col gap-5">
            <input
              placeholder="Destination name *"
              className={inputClass}
              value={name}
              onChange={(e) => setName(e.target.value)}
            />

            <input
              placeholder="Location (city, country)"
              className={inputClass}
              value={location}
              onChange={(e) => setLocation(e.target.value)}
            />

            <div className="grid grid-cols-2 gap-4">
              <div className="flex flex-col gap-1">
                <label className="text-white/50 text-xs pl-1">Arrival date</label>
                <input
                  type="date"
                  className={inputClass}
                  value={startDate}
                  min={tripStart}
                  max={tripEnd}
                  onChange={(e) => setStartDate(e.target.value)}
                />
              </div>
              <div className="flex flex-col gap-1">
                <label className="text-white/50 text-xs pl-1">Departure date</label>
                <input
                  type="date"
                  className={inputClass}
                  value={endDate}
                  min={tripStart}
                  max={tripEnd}
                  onChange={(e) => setEndDate(e.target.value)}
                />
              </div>
            </div>

            <textarea
              placeholder="Description"
              className={`${inputClass} min-h-[80px] resize-y`}
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />

            <textarea
              placeholder="Notes"
              className={`${inputClass} min-h-[70px] resize-y`}
              value={note}
              onChange={(e) => setNote(e.target.value)}
            />

            <div className="flex justify-end gap-3 mt-2">
              <button
                onClick={() => navigate(-1)}
                className="px-5 py-2 rounded-xl border border-white/20 text-gray-300 hover:bg-white/10 transition"
              >
                Cancel
              </button>
              <button
                onClick={handleSubmit}
                disabled={loading}
                className="px-6 py-3 rounded-xl bg-gradient-to-r from-emerald-400 via-green-500 to-teal-500 text-white font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 transition-all duration-200 disabled:opacity-60"
              >
                {loading ? "Saving..." : isEdit ? "Save Changes" : "Add Destination"}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}