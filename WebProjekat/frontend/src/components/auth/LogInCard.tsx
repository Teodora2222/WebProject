import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";
import type { AuthProps } from "../../types/props/auth/AuthProps";
import toast from "react-hot-toast";

export function LogInCard({ usersApi }: AuthProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const { login, isAuthenticated, user } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated && user) {
      navigate("/home");
    }
  }, [isAuthenticated, navigate, user]);

  const handleLogin = async () => {
    try {
      const res = await usersApi.login(email, password);

      if (res.success && res.token) {
        toast.success("Welcome back ✈️");
        login(res.token);
      } else {
        toast.error(res.message || "Wrong credentials.");
      }
    } catch (err: unknown) {
      const message =
        typeof err === "object" &&
        err !== null &&
        "response" in err &&
        typeof (err as any).response?.data?.message === "string"
          ? (err as any).response.data.message
          : "Login failed. Please try again.";

      toast.error(message);
      console.log(err);
    }
  };
  
return (
  <div className="bg-white shadow-2xl rounded-2xl p-8 w-[380px] flex flex-col gap-5">

    <div className="text-center">
      <h1 className="text-3xl font-bold text-blue-600">
        ✈️ Travel Planner
      </h1>
      <p className="text-gray-500 text-sm mt-1">
        Plan your next adventure
      </p>
    </div>

    <input
      type="text"
      placeholder="Email"
      className="px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
      value={email}
      onChange={(e) => setEmail(e.target.value)}
    />

    <input
      type="password"
      placeholder="Password"
      className="px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
      value={password}
      onChange={(e) => setPassword(e.target.value)}
    />

    <button
      className="bg-blue-500 hover:bg-blue-600 text-white py-2 rounded-lg font-semibold transition"
      onClick={handleLogin}
    >
      Log In
    </button>

    <p className="text-center text-sm text-gray-600">
      Don’t have an account?
      <Link to="/register" className="text-blue-600 ml-1 hover:underline">
        Register
      </Link>
    </p>

  </div>
);
}