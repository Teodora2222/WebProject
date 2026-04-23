import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";

export function HomeContent() {
  const navigate = useNavigate();
  const { user } = useAuth();

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gradient-to-br from-blue-400 via-cyan-300 to-yellow-200 text-center p-6">

      <h1 className="text-4xl font-bold text-gray-800">
        🌍 Welcome to Travel Planner
      </h1>

      <p className="mt-2 text-gray-700">
        Plan your trips, organize activities and track expenses easily.
      </p>

      <div className="mt-6 bg-white/30 backdrop-blur-lg p-6 rounded-xl shadow-lg">
        <p className="text-lg text-gray-800">
          Logged in as: <span className="font-semibold">{user?.email}</span>
        </p>
      </div>

      <div className="flex gap-4 mt-8">
        <button
          className="px-6 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
          onClick={() => navigate("/trips")}
        >
          My Trips
        </button>

        <button
          className="px-6 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600"
          onClick={() => navigate("/create-trip")}
        >
          Create Trip
        </button>
      </div>
    </div>
  );
}