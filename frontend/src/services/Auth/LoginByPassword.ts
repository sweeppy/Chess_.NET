import axios from 'axios';
import { LoginResponse } from '../../models/Responses/Account/LoginResponse';
import { LoginRequest } from '../../models/Requests/Account/LoginRequest';
import { accountClient } from '../apiClient';

export const LoginByPasswordAsync = async (
  request: LoginRequest
): Promise<LoginResponse> => {
  try {
    const response = await accountClient.post<LoginResponse>(
      '/Account/LoginByPassword',
      request,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
    const data = response.data;
    if (data.jwtToken !== undefined && data.isSuccess) {
      localStorage.setItem('jwtToken', data.jwtToken);
      return data;
    }

    console.error('Failed to get jwtToken from loginResponse');
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
