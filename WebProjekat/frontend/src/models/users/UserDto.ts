import type { UserRole } from "../../enums/UserRole";

export interface UserDto {
    id? : number;
    firstName : string;
    lastName : string;
    email?:string;
    password?:string;
    createdAt?:string;
    role?: UserRole;
}