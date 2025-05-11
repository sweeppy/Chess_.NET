import { accountClient } from '../apiClient';

export const AddNewUserAndSendVerificationCodeAsync = async (email: string) => {
  try {
    await accountClient.post('/Account/AddNewUser', email, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
  } catch (error: any) {
    console.error(error);
    throw new Error(
      'An error was occurred while trying to register you in system. \
      Please try again or contact support.'
    );
  }
};
