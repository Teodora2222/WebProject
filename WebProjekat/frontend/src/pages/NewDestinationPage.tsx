import { DestinationForm } from "../components/destination/DestinationForm";
import { DestinationApi } from "../api/destination/DestinationApi";
import { useAuth } from "../hooks/useAuth";
import { TravelPlanApi } from "../api/travel/TravelPlanApi";
 
export function NewDestinationPage() {
  const { token } = useAuth();
  const destinationApi = new DestinationApi(token ?? "");
  const travelPlanApi  = new TravelPlanApi(token ?? "");
  return <DestinationForm destinationApi={destinationApi}  travelPlanApi={travelPlanApi}/>;
}