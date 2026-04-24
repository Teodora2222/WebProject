import { useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import type { AuthProps } from "../../types/props/auth/AuthProps";

export function RegisterCard({ usersApi }: AuthProps) {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const handleRegister = async () => {
    if (!firstName || !lastName || !email || !password) {
      toast.error("Please fill in all fields.");
      return;
    }

    if (password !== confirmPassword) {
      toast.error("Passwords do not match.");
      return;
    }

    if (password.length < 6) {
      toast.error("Password must be at least 6 characters.");
      return;
    }

    setLoading(true);

    try {
      const res = await usersApi.register(firstName, lastName, email, password);

      if (res.success) {
        toast.success("Account created ✈️");
        navigate("/");
      } else {
        toast.error(res.message || "Registration failed.");
      }
    } catch {
      toast.error("Something went wrong.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="w-full max-w-xl bg-white/10 backdrop-blur-2xl border border-white/20 rounded-3xl p-10 shadow-2xl">

      <div className="text-center mb-8">
       
        <h1 className="text-3xl font-bold text-white">Create Account</h1>
        <p className="text-gray-300 text-sm mt-1">
          Start planning your next journey
        </p>
      </div>

      <div className="flex flex-col gap-5">

        <div className="grid grid-cols-2 gap-4">
          <input
            placeholder="First name"
            className="px-4 py-3 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />

          <input
            placeholder="Last name"
            className="px-4 py-3 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </div>

        <input
          type="email"
          placeholder="Email"
          className="px-4 py-3 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          className="px-4 py-3 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <input
          type="password"
          placeholder="Confirm password"
          className="px-4 py-3 rounded-xl bg-white/20 text-white placeholder-gray-300 outline-none focus:ring-2 focus:ring-green-400"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />

        <button
          onClick={handleRegister}
          disabled={loading}
          className="mt-3 py-3 rounded-xl bg-green-500 hover:bg-green-600 text-white font-semibold transition disabled:opacity-60"
        >
          {loading ? "Creating..." : "Create Account"}
        </button>
      </div>

      <p className="text-center text-gray-300 mt-6 text-sm">
        Already have an account?
        <span
          onClick={() => navigate("/")}
          className="ml-1 text-green-400 cursor-pointer hover:underline"
        >
          Sign in
        </span>
      </p>
    </div>
  );
}