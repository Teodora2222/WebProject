import React, { createContext, useState, useEffect, type ReactNode } from 'react';
import { jwtDecode } from 'jwt-decode';
import type { AuthContextType } from '../types/auth/AuthContextType';
import type { AuthUser } from '../types/auth/AuthUser';
import { saveValueByKey,removeValueByKey,readValueByKey } from '../helpers/auth/localStorage';
import type { JwtTokenClaims } from '../types/auth/JwtTokenClaims';

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const decodeJWT = (token: string): JwtTokenClaims | null => {
    try {
        const decoded = jwtDecode<JwtTokenClaims>(token);
        
        if (decoded.sub && decoded.email && decoded.role) {
            return {
                sub: decoded.sub,       
                email: decoded.email,
                role: decoded.role
            };
        }
        
        return null;
    } catch (error) {
        console.error('Error with JWT tokena:', error);
        return null;
    }
};

const isTokenExpired = (token: string): boolean => {
    try {
        const decoded = jwtDecode(token);
        const currentTime = Date.now() / 1000;
        
        return decoded.exp ? decoded.exp < currentTime : false;
    } catch {
        return true;
    }
};

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<AuthUser | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const savedToken = readValueByKey("authToken");
        
        if (savedToken) {
            if (isTokenExpired(savedToken)) {
                removeValueByKey("authToken");
                removeValueByKey("role");
                setIsLoading(false);
                return;
            }
            
            const claims = decodeJWT(savedToken);
            if (claims) {
                setToken(savedToken);
                setUser({
                    id: claims.sub,
                    email: claims.email,
                    role: claims.role
                });
                saveValueByKey("role", claims.role);
            } else {
                removeValueByKey("authToken");
                removeValueByKey("role");
            }
        }
        
        setIsLoading(false);
    }, []);

    const login = (newToken: string) => {
        const claims = decodeJWT(newToken);
        console.log(`claims - > ${claims}`)
        if (claims && !isTokenExpired(newToken)) {
            setToken(newToken);
            setUser({
                id: claims.sub,
                email: claims.email,
                role: claims.role
            });
            saveValueByKey("authToken", newToken);
            saveValueByKey("role", claims.role);
        } else {
            console.error('Invalid token');
        }
    };

    const logout = () => {
        setToken(null);
        setUser(null);
        removeValueByKey("authToken");
        removeValueByKey("role");
    };

    const isAuthenticated = !!user && !!token;

    const value: AuthContextType = {
        user,
        token,
        login,
        logout,
        isAuthenticated,
        isLoading
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext;