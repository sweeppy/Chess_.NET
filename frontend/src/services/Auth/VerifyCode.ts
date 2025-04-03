import axios from 'axios';
import { VerifyResponse } from '../../models/Account/Responses/Account/VerifyResponse';

export const VerifyCodeAsync = async (
  email: string,
  code: string
): Promise<VerifyResponse> => {
  try {
    const response = await axios.put<VerifyResponse>(
      'http://localhost:5096/api/Account/VerifyCode',
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
