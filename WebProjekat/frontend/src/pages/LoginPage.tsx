import { LogInCard } from "../components/auth/LogInCard";
import type { AuthProps } from "../types/props/auth/AuthProps";

export function LoginPage({ usersApi }: AuthProps) {
  return (
    <div
      className="w-screen min-h-screen flex items-center justify-center bg-cover bg-center"
    >
      <LogInCard usersApi={usersApi} />
    </div>
  );
}