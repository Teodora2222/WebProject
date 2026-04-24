import type { CreateTravelPlanDto } from "../../models/travel/CreateTravelPlanDto";
import type { TravelPlanDto } from "../../models/travel/TravelPlanDto";
import type { UpdateTravelPlanDto } from "../../models/travel/UpdateTravelPlanDto";
import type { ITravelPlanApi } from "./ITravelPlanApi";
import axios from "axios";
import type { AxiosInstance } from "axios";

export class TravelPlanApi implements ITravelPlanApi {

    private readonly axiosInstance: AxiosInstance;

    constructor(token: string) {
        this.axiosInstance = axios.create({
            baseURL: import.meta.env.VITE_TRIP_SERVICE_API,
            headers: { Authorization: `Bearer ${token}` },
        });
    }

    async createTravelPlan(travelPlan: CreateTravelPlanDto): Promise<TravelPlanDto> {
        const result = await this.axiosInstance.post<TravelPlanDto>(`/api/travel-plans`, travelPlan);
        return result.data;
    }
    async updateTravelPlan(id: number, travelPlan: UpdateTravelPlanDto): Promise<boolean> {
        const result = await this.axiosInstance.put<boolean>(`/api/travel-plans/${id}`, travelPlan);
        return result.data;
    }
    async deleteTravelPlan(id: number): Promise<boolean> {
        const result = await this.axiosInstance.delete<boolean>(`/api/travel-plans/${id}`);
        return result.data;
    }
    async getTravelPlan(id: number): Promise<TravelPlanDto> {
        const result = await this.axiosInstance.get<TravelPlanDto>(`/api/travel-plans/${id}`);
        return result.data;
    }
    async getAllTravelPlans(): Promise<TravelPlanDto[]> {
        const result = await this.axiosInstance.get<TravelPlanDto[]>(`/api/travel-plans`);
        return result.data;
    }
}