import type { CheckListItemDto } from "../../models/checkListItem/CheckListItemDto";
import type { CreateCheckListItemDto } from "../../models/checkListItem/CreateCheckListItemDto";
import type { UpdateCheckListItemDto } from "../../models/checkListItem/UpdateCheckListItemDto";

export interface ICheckListItemAPi {

    createCheckList(travelPlanId : number, checkListItem :CreateCheckListItemDto) : Promise<CheckListItemDto>;

    deleteCheckList(travelPlanId : number,id : number) : Promise<boolean>;

    toggleCheckList(id : number,checkListItem : UpdateCheckListItemDto,travelPlanId : number) : Promise<boolean>;

    getAllCheckLists(travelPlanId : number) : Promise<CheckListItemDto[]>;
}