import { useEffect } from 'react';
import { useRouter } from 'next/router';
import { useAuth } from '@/contexts/authContext';

interface PrivateRouteProps {
    children: React.ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
    const { isAuthenticated, loading } = useAuth();
    const router = useRouter();

    useEffect(() => {
        if (!loading && !isAuthenticated) {
            router.replace('/login');
        }
    }, [isAuthenticated, loading]);

    if (loading) {
        return null;
    }

    return <>{isAuthenticated && children}</>;
}

export default PrivateRoute;
