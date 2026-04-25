import type { CreateDestinationDto } from "../../models/destination/CreateDestinationDto";
import type { DestinationDto } from "../../models/destination/DestinationDto";
import type { UpdateDestinationDto } from "../../models/destination/UpdateDestinationDto";

export interface IDestinationApi {
    createDestination(travelPlanId: number, destination: CreateDestinationDto): Promise<DestinationDto>;
    updateDestination(travelPlanId: number, id: number, destination: UpdateDestinationDto): Promise<boolean>;
    deleteDestination(travelPlanId: number, id: number): Promise<boolean>;
    getDestination(travelPlanId: number, id: number): Promise<DestinationDto>;
    getAllDestinations(travelPlanId: number): Promise<DestinationDto[]>;
}