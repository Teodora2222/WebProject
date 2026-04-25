import { ActivityForm } from "../components/activity/ActivityForm";
import { ActivityApi } from "../api/activity/ActivityApi";
import { useAuth } from "../hooks/useAuth";
import { TravelPlanApi } from "../api/travel/TravelPlanApi";
 
export function NewActivityPage() {
  const { token } = useAuth();
  const activityApi = new ActivityApi(token ?? "");
  const travelPlanApi = new TravelPlanApi(token ?? "");
  return <ActivityForm activityApi={activityApi} travelPlanApi={travelPlanApi} />;
}