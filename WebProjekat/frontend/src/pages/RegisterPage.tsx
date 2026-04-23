import type { AuthProps } from "../types/props/auth/AuthProps";
import { RegisterCard } from "../components/auth/RegisterCard";

export function RegisterPage({ usersApi }: AuthProps) {
  return (
    <div className="w-screen min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-400 via-cyan-300 to-yellow-200">
      <RegisterCard usersApi={usersApi} />
    </div>
  );
}
