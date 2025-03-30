import axios from 'axios';

export const OnStartGame = async () => {
    try {
        const token = localStorage.getItem('jwtToken');
        if (!token) throw new Error('No token found');

        console.log('Token being sent:', token); // Проверьте в консоли браузера
        const response = await axios.post(
            'http://localhost:5011/api/ChessMovement/OnGameStart',
            {},
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );
        return response.data;
    } catch (error: any) {
        console.error('Error details:', {
            message: error.message,
            response: error.response?.data,
            status: error.response?.status,
        });
        throw error;
    }
};
