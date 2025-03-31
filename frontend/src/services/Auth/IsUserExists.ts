import axios from 'axios';
import { IsUserExistsAndEmailConfirmedResponse } from '../../models/Account/Responses/IsUserExistsRequest';

export const isUserExistsAndEmailConfirmedAsync = async (
    email: string
): Promise<IsUserExistsAndEmailConfirmedResponse> => {
    try {
        const response =
            await axios.post<IsUserExistsAndEmailConfirmedResponse>(
                'http://localhost:5096/api/Account/IsUserExistsAndEmailConfirmed',
                email,
                {
                    headers: {
                        'Content-Type': 'application/json',
                    },
                }
            );
        const data = response.data;

        return data;
    } catch (error: any) {
        console.error(error);
        throw new Error(
            'En error occurred while checking your account. Please try again or contact support.'
        );
    }
};
