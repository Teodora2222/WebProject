import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import type { AuthProps } from "../../types/props/auth/AuthProps";

export function RegisterCard({ usersApi }: AuthProps) {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleRegister = async () => {
    try {
      const res = await usersApi.register(firstName, lastName, email, password);

      if (res.success) {
        toast.success("Account created 🎉");
        navigate("/");
      } else {
        toast.error(res.message || "Registration failed");
      }
    } catch {
      toast.error("Something went wrong");
    }
  };

  return (
  <div className="bg-white shadow-2xl rounded-2xl p-8 w-[380px] flex flex-col gap-4">

    <h1 className="text-2xl font-bold text-center text-blue-600">
      🌍 Create Account
    </h1>

    <input placeholder="First Name" className="p-2 border rounded-lg" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
    <input placeholder="Last Name" className="p-2 border rounded-lg" value={lastName} onChange={(e) => setLastName(e.target.value)} />
    <input placeholder="Email" className="p-2 border rounded-lg" value={email} onChange={(e) => setEmail(e.target.value)} />
    <input type="password" placeholder="Password" className="p-2 border rounded-lg" value={password} onChange={(e) => setPassword(e.target.value)} />

    <button
      className="bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600"
      onClick={handleRegister}
    >
      Register
    </button>

    <p className="text-center text-sm text-gray-600">
      Already have account?
      <Link to="/" className="text-blue-600 ml-1">Login</Link>
    </p>

  </div>
);
}