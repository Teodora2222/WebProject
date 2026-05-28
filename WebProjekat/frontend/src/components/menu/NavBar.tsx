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
    <nav className="sticky top-0 z-50 bg-gradient-to-r from-[#022c22]/90 to-[#020617]/90 backdrop-blur-xl border-b border-white/10">
      <div className="max-w-6xl mx-auto flex items-center justify-between px-6 py-4">

        {/* LOGO */}
        <div
          onClick={() => navigate("/home")}
          className="flex items-center gap-3 cursor-pointer"
        >
          <div className="w-9 h-9 rounded-xl bg-green-500 flex items-center justify-center text-black font-bold">
            ✈
          </div>
          <span className="text-white font-semibold text-lg">
            Travel Planner
          </span>
        </div>

        <div className="flex-1" />

        <div className="relative">
          <div
            onClick={() => setIsOpen(!isOpen)}
            className="w-10 h-10 rounded-full bg-green-500 flex items-center justify-center text-black font-semibold cursor-pointer"
          >
            {user?.email?.charAt(0).toUpperCase()}
          </div>

          {isOpen && (
            <div className="absolute right-0 mt-3 w-56 bg-[#020617] border border-white/10 rounded-xl shadow-xl p-2 text-sm">

              <p className="text-white/40 px-3 py-2 text-xs border-b border-white/10 mb-1">
                {user?.email}
              </p>

              {user?.role === UserRole.ADMIN && (
                <button
                  onClick={() => navigate("/admin")}
                  className="w-full text-left px-3 py-2 hover:bg-white/5 rounded-lg text-yellow-400"
                >
                  Admin Panel
                </button>
              )}

              <button
                onClick={handleLogout}
                className="w-full text-left px-3 py-2 hover:bg-white/5 rounded-lg text-red-400 mt-1 border-t border-white/10"
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