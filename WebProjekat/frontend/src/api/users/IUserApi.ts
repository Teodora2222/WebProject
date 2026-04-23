import type { AuthResponse } from "../../types/auth/AuthResponse";
import type { ApiResponse } from "../../types/auth/ApiResponse";
import type { UserDto } from "../../models/users/UserDto";

export interface IUserApi {
    login(email: string, password: string): Promise<AuthResponse>;
    register(firstName: string, lastName: string, email: string, password: string): Promise<AuthResponse>;
    getById(id: number): Promise<UserDto>;
    getAll(): Promise<UserDto[]>;
    deleteUserById(id: number): Promise<AuthResponse>;
    updateUser(user:UserDto):Promise<ApiResponse>;
}