import type { CreateDestinationDto } from "../../models/destination/CreateDestinationDto";
import type { DestinationDto } from "../../models/destination/DestinationDto";
import type { UpdateDestinationDto } from "../../models/destination/UpdateDestinationDto";
import type { IDestinationApi } from "./IDestinationApi";
import axios from "axios";
import type { AxiosInstance } from "axios";

export class DestinationApi implements IDestinationApi {

    private readonly axiosInstance: AxiosInstance;

    constructor(token: string) {
        this.axiosInstance = axios.create({
            baseURL: import.meta.env.VITE_TRIP_SERVICE_API,
            headers: { Authorization: `Bearer ${token}` },
        });
    }

    async createDestination(travelPlanId: number, destination: CreateDestinationDto): Promise<DestinationDto> {
        const result = await this.axiosInstance.post<DestinationDto>(
            `/api/travel-plans/${travelPlanId}/destinations`, destination);
        return result.data;
    }

    async updateDestination(travelPlanId: number, id: number, destination: UpdateDestinationDto): Promise<boolean> {
        const result = await this.axiosInstance.put<boolean>(
            `/api/travel-plans/${travelPlanId}/destinations/${id}`, destination);
        return result.data;
    }

    async deleteDestination(travelPlanId: number, id: number): Promise<boolean> {
        const result = await this.axiosInstance.delete<boolean>(
            `/api/travel-plans/${travelPlanId}/destinations/${id}`);
        return result.data;
    }

    async getDestination(travelPlanId: number, id: number): Promise<DestinationDto> {
        const result = await this.axiosInstance.get<DestinationDto>(
            `/api/travel-plans/${travelPlanId}/destinations/${id}`);
        return result.data;
    }

    async getAllDestinations(travelPlanId: number): Promise<DestinationDto[]> {
        const result = await this.axiosInstance.get<DestinationDto[]>(
            `/api/travel-plans/${travelPlanId}/destinations`);
        return result.data;
    }
}