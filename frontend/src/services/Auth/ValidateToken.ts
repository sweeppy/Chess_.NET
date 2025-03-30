import axios from 'axios';

export const validateToken = async () => {
    try {
        const token = localStorage.getItem('jwtToken');

        if (!token) {
            console.warn('token not found');
            return false;
        }

        const response = await axios.get(
            'http://localhost:5096/api/Account/ValidateToken',
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );

        return response.data.isValid;
    } catch (error) {
        console.error('Token validation failed:', error);
        localStorage.removeItem('jwtToken');
        return false;
    }
};
