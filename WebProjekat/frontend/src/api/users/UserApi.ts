import type { UserDto } from "../../models/users/UserDto";
import type { ApiResponse } from "../../types/auth/ApiResponse";
import type { AuthResponse } from "../../types/auth/AuthResponse";
import type { IUserApi } from "./IUserApi";
import axios from "axios";
import type { AxiosInstance } from "axios";

export class UserApi implements IUserApi {
    
    private readonly axiosInstance: AxiosInstance;

    constructor(token?: string) {
        this.axiosInstance = axios.create({
            baseURL: import.meta.env.VITE_API_URL,
            headers: token ? { Authorization: `Bearer ${token}` } : {},
        });
    }
    
    async login(email: string, password: string): Promise<AuthResponse> {
        const result = await this.axiosInstance.post<AuthResponse>(`/api/auth/login`, { email, password });
        return result.data;
    }

    async register(firstName: string, lastName: string, email: string, password: string): Promise<AuthResponse> {
        const user: UserDto = { firstName, lastName, email, password };
        const result = await this.axiosInstance.post<AuthResponse>(`/api/auth/register`, user);
        return result.data;
    }

    async getById(id: number): Promise<UserDto> {
        const result = await this.axiosInstance.get<UserDto>(`/api/users/${id}`);
        return result.data;
    }

    async getAll(): Promise<UserDto[]> {
        const result = await this.axiosInstance.get<UserDto[]>(`/api/users`);
        return result.data;
    }

    async deleteUserById(id: number): Promise<AuthResponse> {
        const result = await this.axiosInstance.delete<AuthResponse>(`/api/users/${id}`);
        return result.data;
    }

    async updateUser(user: UserDto): Promise<ApiResponse> {
        const result = await this.axiosInstance.put<ApiResponse>(`/api/users/${user.id}`, user);
        return result.data;
    }
}