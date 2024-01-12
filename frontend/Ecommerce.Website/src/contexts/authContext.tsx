import api from "@/services/apiAuthorization";
import { useRouter } from "next/router";
import { createContext, ReactNode, useState, useEffect, useContext } from "react";

interface AuthContextProps {
    isAuthenticated: boolean;
    loading: boolean;
    login: (emailOrPhoneNumber: string, password: string) => Promise<void>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const router = useRouter();
    const [loading, setLoading] = useState(true);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const storedToken = localStorage.getItem('SESSIONJWT');

        if (storedToken) {
            setIsAuthenticated(true);
        }

        setLoading(false);
    }, [])

    async function login(emailOrPhoneNumber: string, password: string) {
        const response = await api.post('/login', { emailOrPhoneNumber, password })

        localStorage.setItem('SESSIONJWT', response.data.message);
        setIsAuthenticated(true);
        router.push('/home');
    }

    function logout() {
        localStorage.removeItem('SESSIONJWT');
        setIsAuthenticated(false);
        router.push('/login');
    }

    return (
        <AuthContext.Provider value={{ isAuthenticated, loading, login, logout }}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within a AuthProvider');
    }

    return context;
}
