import { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { validateToken } from '../../services/Auth/ValidateToken';

const ProtectedRoute = () => {
    const [isValid, setIsValid] = useState<boolean | null>(null);

    useEffect(() => {
        const checkToken = async () => {
            const isValidToken = await validateToken();
            setIsValid(isValidToken);
        };
        checkToken();
    }, []);

    if (isValid === null) {
        return <div>Loading...</div>;
    }

    return isValid ? <Outlet /> : <Navigate to="/login" replace />;
};

export default ProtectedRoute;
