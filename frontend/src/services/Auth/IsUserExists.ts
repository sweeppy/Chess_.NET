import { IsUserExistsAndEmailConfirmedResponse } from '../../models/Responses/Account/IsUserExistsRequest';
import { accountClient } from '../apiClient';

export const isUserExistsAndEmailConfirmedAsync = async (
  email: string
): Promise<IsUserExistsAndEmailConfirmedResponse> => {
  try {
    const response =
      await accountClient.post<IsUserExistsAndEmailConfirmedResponse>(
        '/Account/IsUserExistsAndEmailConfirmed',
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
