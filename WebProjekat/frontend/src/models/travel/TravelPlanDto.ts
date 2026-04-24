
export interface TravelPlanDto {
    id : number;
    userId : number;
    title : string;
    description? : string;
    note? : string;
    startDate : string;
    endDate : string;
    budget : number;
    createdAt : string;
}