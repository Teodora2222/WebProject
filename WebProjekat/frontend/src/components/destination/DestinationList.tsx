import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { DestinationDto } from "../../models/destination/DestinationDto";
import type { IDestinationApi } from "../../api/destination/IDestinationApi";
import { ConfirmModal } from "../modal/ConfirmModal";

interface Props {
  destinationApi: IDestinationApi;
}

export function DestinationsList({ destinationApi }: Props) {
  const { id: travelPlanId } = useParams();
  const [destinations, setDestinations] = useState<DestinationDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [deleteId, setDeleteId] = useState<number | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    if (travelPlanId) fetchDestinations();
  }, [travelPlanId]);

  const fetchDestinations = async () => {
    try {
      const data = await destinationApi.getAllDestinations(Number(travelPlanId));
      setDestinations(data);
    } catch {
      toast.error("Failed to load destinations.");
    } finally {
      setLoading(false);
    }
  };

const handleDelete = (destId: number, e: React.MouseEvent) => {
  e.stopPropagation();
  setDeleteId(destId);
};

  const confirmDelete = async () => {
  if (!deleteId) return;

  try {
    await destinationApi.deleteDestination(
      Number(travelPlanId),
      deleteId
    );

    setDestinations(prev =>
      prev.filter(d => d.id !== deleteId)
    );

    toast.success("Destination deleted.");
  } catch {
    toast.error("Failed to delete.");
  } finally {
    setDeleteId(null);
  }
};

  const formatDate = (dateStr?: string) => {
    if (!dateStr) return "—";
    return new Date(dateStr).toLocaleDateString("en-GB", { day: "numeric", month: "short", year: "numeric" });
  };

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
          <h2 className="text-xl font-bold text-white">Destinations</h2>
          <p className="text-white/50 text-sm">{destinations.length} place{destinations.length !== 1 ? "s" : ""}</p>
        </div>
        <button
          onClick={() => navigate(`/trips/${travelPlanId}/destinations/new`)}
         className="px-5 py-2 rounded-lg text-sm font-medium
bg-green-500/20 text-green-300
border border-green-400/30
hover:bg-green-500/30 hover:border-green-300
transition"
         >
          + Add Destination
        </button>
      </div>

      {destinations.length === 0 ? (
        <div className="text-center py-16 bg-white/5 rounded-3xl border border-dashed border-white/20 text-white/40">
          No destinations added yet.
        </div>
      ) : (
        <div className="flex flex-col gap-4">
          {destinations.map((dest) => (
            <div
              key={dest.id}
              onClick={() => navigate(`/trips/${travelPlanId}/destinations/${dest.id}/edit`)}
              className="group bg-[#064e3b]/30 backdrop-blur-md border border-white/10 rounded-2xl p-5 hover:border-green-500/50 transition-all duration-300 cursor-pointer flex items-start justify-between gap-4"
            >
              <div className="flex items-start gap-4">
                <div className="w-10 h-10 rounded-xl bg-green-500/20 flex items-center justify-center text-green-400 font-bold text-lg flex-shrink-0">
                  📍
                </div>
                <div>
                  <h3 className="text-white font-bold group-hover:text-green-400 transition-colors">
                    {dest.name}
                  </h3>
                  {dest.location && (
                    <p className="text-white/50 text-sm mt-0.5">{dest.location}</p>
                  )}
                  <div className="flex gap-4 mt-2 text-xs text-white/40">
                    {dest.startDate && <span>✈️ Arrival: {formatDate(dest.startDate)}</span>}
                    {dest.endDate && <span>🏠 Departure: {formatDate(dest.endDate)}</span>}
                  </div>
                  {dest.description && (
                    <p className="text-white/40 text-sm mt-2 line-clamp-2">{dest.description}</p>
                  )}
                </div>
              </div>

              <button
                onClick={(e) => handleDelete(dest.id, e)}
                className="opacity-0 group-hover:opacity-100 p-2 bg-red-500/20 hover:bg-red-500 rounded-xl text-white transition-all flex-shrink-0"
              >
                <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                </svg>
              </button>
            </div>
          ))}
        </div>
      )}

      {deleteId && (
  <ConfirmModal
    title="Delete destination"
    message="This action cannot be undone."
    onConfirm={confirmDelete}
    onCancel={() => setDeleteId(null)}
  />
)}
    </div>
  );
}