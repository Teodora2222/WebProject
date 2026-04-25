import type { ActivityDto } from "../../models/activity/ActivityDto";
import type { CreateActivityDto } from "../../models/activity/CreateActivityDto";
import type { UpdateActivityDto } from "../../models/activity/UpdateActivityDto";
import type { IActivityApi } from "./IActivityApi";
import axios from "axios";
import type { AxiosInstance } from "axios";

export class ActivityApi implements IActivityApi {

    private readonly axiosInstance: AxiosInstance;

    constructor(token: string) {
        this.axiosInstance = axios.create({
            baseURL: import.meta.env.VITE_TRIP_SERVICE_API,
            headers: { Authorization: `Bearer ${token}` },
        });
    }

    async createActivity(travelPlanId: number, activity: CreateActivityDto): Promise<ActivityDto> {
        console.log("Sending activity:", JSON.stringify(activity));
        const result = await this.axiosInstance.post<ActivityDto>(
            `/api/travel-plans/${travelPlanId}/activities`,
                    activity 
        );
        return result.data;
    }

    async updateActivity(travelPlanId: number, id: number, activity: UpdateActivityDto): Promise<boolean> {
        const result = await this.axiosInstance.put<boolean>(
            `/api/travel-plans/${travelPlanId}/activities/${id}`, activity);
        return result.data;
    }

    async deleteActivity(travelPlanId: number, id: number): Promise<boolean> {
        const result = await this.axiosInstance.delete<boolean>(
            `/api/travel-plans/${travelPlanId}/activities/${id}`);
        return result.data;
    }

    async getActivity(travelPlanId: number, id: number): Promise<ActivityDto> {
        const result = await this.axiosInstance.get<ActivityDto>(
            `/api/travel-plans/${travelPlanId}/activities/${id}`);
        return result.data;
    }

    async getAllActivities(travelPlanId: number): Promise<ActivityDto[]> {
        const result = await this.axiosInstance.get<ActivityDto[]>(
            `/api/travel-plans/${travelPlanId}/activities`);
        return result.data;
    }

    async getAllActivitiesByDate(travelPlanId: number, date: string): Promise<ActivityDto[]> {
        const result = await this.axiosInstance.get<ActivityDto[]>(
            `/api/travel-plans/${travelPlanId}/activities/by-date?date=${date}`);
        return result.data;
    }
}