import type { CreateTravelPlanDto } from "../../models/travel/CreateTravelPlanDto";
import type { TravelPlanDto } from "../../models/travel/TravelPlanDto";
import type { UpdateTravelPlanDto } from "../../models/travel/UpdateTravelPlanDto";

export interface ITravelPlanApi {
    
    createTravelPlan(travelPlan : CreateTravelPlanDto) : Promise<TravelPlanDto>;

    updateTravelPlan(id : number,travelPlan : UpdateTravelPlanDto) : Promise<boolean>;

    deleteTravelPlan(id : number) : Promise<boolean>;

    getTravelPlan(id : number) : Promise<TravelPlanDto>;

    getAllTravelPlans() : Promise<TravelPlanDto[]>;
}