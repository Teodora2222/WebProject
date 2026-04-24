import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";
import type { AuthProps } from "../../types/props/auth/AuthProps";
import toast from "react-hot-toast";

export function LogInCard({ usersApi }: AuthProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const { login } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async () => {
    if (!email || !password) {
      toast.error("Please fill in all fields.");
      return;
    }

    setLoading(true);

    try {
      const res = await usersApi.login(email, password);

      if (res.success && res.token) {
        toast.success("Welcome back ✈️");

        login(res.token);

        navigate("/home");
      } else {
        toast.error(res.message || "Wrong credentials.");
      }
    } catch (err: any) {
      const message =
        err?.response?.data?.message || "Login failed. Please try again.";
      toast.error(message);
    } finally {
      setLoading(false);
    }
  };

return (
  <div className="w-full max-w-md">

    <div className="bg-white/10 backdrop-blur-xl border border-white/20 rounded-2xl p-8 shadow-xl">

      <div className="text-center mb-8">
        
        <h1 className="text-2xl font-semibold text-white">
          Travel Planner
        </h1>
        <p className="text-gray-300 text-sm mt-1">
          Plan your next journey
        </p>
      </div>

      <div className="flex flex-col gap-4">

        <input
          placeholder="Email"
          className="px-4 py-3 rounded-lg bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          className="px-4 py-3 rounded-lg bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <button
          onClick={handleLogin}
          className="mt-2 py-3 rounded-lg bg-green-500 hover:bg-green-600 text-white font-medium transition"
        >
          {loading ? "Signing in..." : "Sign In"}
        </button>
      </div>

      <p className="text-center text-gray-300 text-sm mt-6">
        Don’t have an account?
        <span
          onClick={() => navigate("/register")}
          className="text-green-400 ml-1 cursor-pointer hover:underline"
        >
          Create one
        </span>
      </p>

    </div>
  </div>
);
}