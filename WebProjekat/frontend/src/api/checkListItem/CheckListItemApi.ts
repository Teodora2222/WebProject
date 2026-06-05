import type { CheckListItemDto } from "../../models/checkListItem/CheckListItemDto";
import type { CreateCheckListItemDto } from "../../models/checkListItem/CreateCheckListItemDto";
import type { UpdateCheckListItemDto } from "../../models/checkListItem/UpdateCheckListItemDto";
import type { ICheckListItemAPi } from "./ICheckListItemApi";
import axios from "axios";
import type { AxiosInstance } from "axios";


export class CheckListItemApi implements ICheckListItemAPi {
     
    private readonly axiosInstance: AxiosInstance;

    constructor(token: string) {
        this.axiosInstance = axios.create({
            baseURL: import.meta.env.VITE_API_URL,
            headers: { Authorization: `Bearer ${token}` },
        });
    }

    async createCheckList(travelPlanId: number,checkListItem: CreateCheckListItemDto): Promise<CheckListItemDto> {
        const result = await this.axiosInstance.post<CheckListItemDto>(
                    `/api/travel-plans/${travelPlanId}/checklist`,checkListItem
                    );
        return result.data;
    }

    async deleteCheckList(travelPlanId: number, id: number): Promise<boolean> {
        const result = await this.axiosInstance.delete<boolean>(
                    `/api/travel-plans/${travelPlanId}/checklist/${id}`
                );
        return result.data;
    }

    async toggleCheckList(id: number, checkListItem: UpdateCheckListItemDto,travelPlanId: number): Promise<boolean> {
       const result = await this.axiosInstance.put<boolean>(
                `/api/travel-plans/${travelPlanId}/checklist/${id}`,
                checkListItem
            );
        return result.data;
    }

    async getAllCheckLists(travelPlanId: number): Promise<CheckListItemDto[]> {
        const result = await this.axiosInstance.get<CheckListItemDto[]>(
                    `/api/travel-plans/${travelPlanId}/checklist`
                );
        return result.data;
    }
    
}