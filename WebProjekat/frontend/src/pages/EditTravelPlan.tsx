import { TravelPlanForm } from "../components/travelPlan/TravelPlanForm";
import { TravelPlanApi } from "../api/travel/TravelPlanApi";
import { useAuth } from "../hooks/useAuth";

export function EditTripPage() {
  const { token } = useAuth();
  const travelPlanApi = new TravelPlanApi(token ?? "");
  
  return <TravelPlanForm travelPlanApi={travelPlanApi} />;
}