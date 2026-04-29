import type { ShareResponseDto } from "../../models/share/ShareResponseDto";
import type { CreateShareDto } from "../../models/share/CreateShareDto";
 
export interface IShareApi {
  createShare(travelPlanId: number, dto: CreateShareDto): Promise<ShareResponseDto>;
}