import type { ActivityDto } from "../../models/activity/ActivityDto";
import type { CreateActivityDto } from "../../models/activity/CreateActivityDto";
import type { UpdateActivityDto } from "../../models/activity/UpdateActivityDto";

export interface IActivityApi {
    createActivity(travelPlanId: number, activity: CreateActivityDto): Promise<ActivityDto>;
    updateActivity(id: number,travelPlanId: number, activity: UpdateActivityDto): Promise<boolean>;
    deleteActivity(travelPlanId: number, id: number): Promise<boolean>;
    getActivity(travelPlanId: number, id: number): Promise<ActivityDto>;
    getAllActivities(travelPlanId: number): Promise<ActivityDto[]>;
    getAllActivitiesByDate(travelPlanId: number, date: string): Promise<ActivityDto[]>;
}