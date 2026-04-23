import { useContext } from "react";
import type { AuthContextType } from "../types/auth/AuthContextType";
import AuthContext from "../context/AuthContext";

export const useAuth = (): AuthContextType => {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth mora biti korišćen unutar AuthProvider-a');
    }
    return context;
};