import axios from 'axios';
import { LoginRequest } from '../../models/Account/Requests/LoginRequest';
import { LoginResponse } from '../../models/Account/Responses/Account/LoginResponse';

export const LoginByPasswordAsync = async (
    request: LoginRequest
): Promise<LoginResponse> => {
    try {
        const response = await axios.post<LoginResponse>(
            'http://localhost:5096/api/Account/LoginByPassword',
            request,
            {
                headers: {
                    'Content-Type': 'application/json',
                },
            }
        );
        const data = response.data;
        console.log(data);
        if (data.jwtToken !== undefined && data.isSuccess) {
            localStorage.setItem('jwtToken', data.jwtToken);
            return response.data;
        }
        console.error(
            'An error occurred while trying to get jwtToken from loginResponse'
        );
        throw new Error(data.message);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            if (error.response) {
                const errorMessage =
                    error.response.data?.message ||
                    'Something went wrong. Please try again or contact support.';
                throw new Error(errorMessage);
            }
        }
        throw new Error(
            'An error occurred while checking your login data. Please try again or contact support.'
        );
    }
};
