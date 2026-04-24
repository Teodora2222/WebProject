import { TravelPlanApi } from "../api/travel/TravelPlanApi";
import { NavBar } from "../components/menu/NavBar";
import { TravelPlansList } from "../components/travelPlan/TravelPlanList";
import { useAuth } from "../hooks/useAuth";

export function HomePage() {
  const { token, user } = useAuth();
  const travelPlanApi = new TravelPlanApi(token ?? "");

  return (
    <div className="h-screen flex flex-col bg-gradient-to-br from-[#020617] via-[#064e3b] to-[#020617] text-white overflow-hidden">

      <NavBar />

      <div className="max-w-6xl mx-auto px-6 pt-6 pb-4 w-full">
        <h1 className="text-3xl md:text-4xl font-bold">
          Welcome back, {user?.email?.split("@")[0]} 👋
        </h1>

        <p className="text-gray-300 mt-2">
          Plan your next adventure, manage your trips and explore the world.
        </p>
      </div>

      <div className="flex-1 overflow-y-auto">
        <TravelPlansList travelPlanApi={travelPlanApi} />
      </div>

    </div>
  );
}