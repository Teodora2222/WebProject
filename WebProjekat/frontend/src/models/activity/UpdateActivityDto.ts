import type { Status } from "../../enums/Status";

export interface UpdateActivityDto {
    name? : string;
    description? : string;
    location? : string;
    date? : string;
    time? : string;
    estimatedCost? : number;
    status? : Status;
}