import type { AuthProps } from "../types/props/auth/AuthProps";
import { RegisterCard } from "../components/auth/RegisterCard";

export function RegisterPage({ usersApi }: AuthProps) {
  return (
    <div className="min-h-screen overflow-hidden flex items-center justify-center relative">

  <div
    className="absolute inset-0 bg-cover bg-center blur-sm scale-110"/>
     <img src="/fr.jpg"
        className="absolute w-full h-full object-cover blur-[2px] brightness-75 scale-105"
      />
      
    <div className="absolute inset-0 bg-gradient-to-br from-[#022c22]/60 via-[#064e3b]/70 to-[#020617]/90" />
      <RegisterCard usersApi={usersApi} />
    </div>
  );
}
