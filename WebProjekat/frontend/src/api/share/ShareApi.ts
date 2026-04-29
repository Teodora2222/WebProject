import axios, { type AxiosInstance } from "axios";
import type { IShareApi } from "./IShareApi";
import type { ShareResponseDto } from "../../models/share/ShareResponseDto";
import type { CreateShareDto } from "../../models/share/CreateShareDto";
 
export class ShareApi implements IShareApi {
  private readonly axiosInstance: AxiosInstance;
 
  constructor(token: string) {
    this.axiosInstance = axios.create({
      baseURL: import.meta.env.VITE_TRIP_SERVICE_API,
      headers: { Authorization: `Bearer ${token}` },
    });
  }
 
  async createShare(travelPlanId: number, dto: CreateShareDto): Promise<ShareResponseDto> {
    const result = await this.axiosInstance.post<ShareResponseDto>(
      `/api/travel-plans/${travelPlanId}/shares`,
      dto
    );
    return result.data;
  }
}
 