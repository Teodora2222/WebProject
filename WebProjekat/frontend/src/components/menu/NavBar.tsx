import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";

export function NavBar() {
  const navigate = useNavigate();
  const { user, logout } = useAuth();
  const [isOpen, setIsOpen] = useState(false);

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <nav className="w-full max-w-4xl flex items-center justify-between bg-white/30 backdrop-blur-lg px-6 py-3 rounded-xl shadow-md">

      {/* LOGO */}
      <div
        className="font-bold text-xl cursor-pointer text-gray-800"
        onClick={() => navigate("/home")}
      >
        ✈️ Travel Planner
      </div>

      {/* LINKS */}
      <div className="flex gap-6 text-gray-700 font-medium">
        <span className="cursor-pointer hover:text-blue-600" onClick={() => navigate("/home")}>
          Home
        </span>

        <span className="cursor-pointer hover:text-blue-600" onClick={() => navigate("/trips")}>
          Trips
        </span>

        <span className="cursor-pointer hover:text-blue-600" onClick={() => navigate("/create-trip")}>
          Create Trip
        </span>
      </div>

      {/* USER */}
      <div className="relative">
        <div
          onClick={() => setIsOpen(!isOpen)}
          className="w-10 h-10 rounded-full bg-blue-500 flex items-center justify-center text-white cursor-pointer"
        >
          {user?.email?.charAt(0).toUpperCase()}
        </div>

        {isOpen && (
          <div className="absolute right-0 mt-2 w-40 bg-white rounded-lg shadow-lg p-2">
            <p className="text-sm text-gray-700 px-2">{user?.email}</p>

            <button
              onClick={handleLogout}
              className="w-full text-left text-red-500 px-2 py-1 hover:bg-gray-100 rounded"
            >
              Logout
            </button>
          </div>
        )}
      </div>
    </nav>
  );
}