import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";
import { UserRole } from "../../enums/UserRole";

export function NavBar() {
  const navigate = useNavigate();
  const { user, logout } = useAuth();
  const [isOpen, setIsOpen] = useState(false);

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
  <nav className="w-full bg-white/10 backdrop-blur-lg shadow-md">

    <div className="max-w-7xl mx-auto flex items-center justify-between px-6 py-3">

      <div
        className="font-bold text-xl cursor-pointer text-white"
        onClick={() => navigate("/home")}
      >
        ✈️ Travel Planner
      </div>

      <div className="flex gap-8 text-white font-medium">
        <span
          onClick={() => navigate("/home")}
          className="cursor-pointer hover:text-green-300 transition"
        >
          Home
        </span>

        <span
          onClick={() => navigate("/trips/new")}
          className="cursor-pointer hover:text-green-300 transition"
        >
          Destination
        </span>

        {user?.role === UserRole.ADMIN && (
          <span
            onClick={() => navigate("/admin")}
            className="cursor-pointer hover:text-red-300 transition"
          >
            Admin
          </span>
        )}
      </div>

      <div className="relative">
        <div
          onClick={() => setIsOpen(!isOpen)}
          className="w-10 h-10 rounded-full bg-gradient-to-br from-emerald-400 to-green-600 flex items-center justify-center text-white cursor-pointer font-semibold"
        >
          {user?.email?.charAt(0).toUpperCase()}
        </div>

        {isOpen && (
          <div className="absolute right-0 mt-2 w-48 bg-white/90 backdrop-blur rounded-xl shadow-lg p-2 text-sm">

            <p className="text-gray-800 px-2 mb-2 truncate">
              {user?.email}
            </p>

            <button
              onClick={handleLogout}
              className="w-full text-left text-red-500 px-2 py-2 hover:bg-gray-100 rounded-lg transition"
            >
              Logout
            </button>

          </div>
        )}
      </div>

    </div>
  </nav>
);
}