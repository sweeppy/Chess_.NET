import { VerifyResponse } from '../../models/Responses/Account/VerifyResponse';
import { accountClient } from '../apiClient';

export const VerifyCodeAsync = async (
  email: string,
  code: string
): Promise<VerifyResponse> => {
  try {
    const response = await accountClient.put<VerifyResponse>(
      '/Account/VerifyCode',
      {
        email,
        code,
      },
      {
        headers: { 'Content-Type': 'application/json' },
      }
    );
    if (response.data.jwtToken) {
      localStorage.setItem('jwtToken', response.data.jwtToken);
    }
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error(
      'An error occurred while verifying your account. Please try again or contact support.'
    );
  }
};
