import { LogInCard } from "../components/auth/LogInCard";
import type { AuthProps } from "../types/props/auth/AuthProps";


export function LoginPage({ usersApi }: AuthProps) {
  return (
    <div className="h-screen w-full relative overflow-hidden">

     <img src="/fr.jpg"
        className="absolute w-full h-full object-cover blur-[2px] brightness-75 scale-105"
      />

  <div className="absolute inset-0 bg-gradient-to-br from-[#022c22]/70 via-[#064e3b]/60 to-[#020617]/80"></div>
      <div className="relative z-10 flex items-center justify-center h-full px-4">
        <LogInCard usersApi={usersApi} />
      </div>
    </div>
  );
}