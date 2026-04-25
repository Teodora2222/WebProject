import { DestinationForm } from "../components/destination/DestinationForm";
import { DestinationApi } from "../api/destination/DestinationApi";
import { useAuth } from "../hooks/useAuth";
 
export function EditDestinationPage() {
  const { token } = useAuth();
  const destinationApi = new DestinationApi(token ?? "");
  return <DestinationForm destinationApi={destinationApi} />;
}