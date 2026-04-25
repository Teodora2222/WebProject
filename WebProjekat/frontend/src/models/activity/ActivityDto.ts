import type { Status } from "../../enums/Status";

export interface ActivityDto {
    id: number;
    travelPlanId: number;  
    name: string;
    description?: string;
    location?: string;
    date: string;
    time?: string;         
    estimatedCost?: number; 
    status?: Status;
}