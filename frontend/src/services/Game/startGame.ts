import axios from 'axios';
import { GameStartResponse } from '../../models/Account/Responses/Chess/GameStartResponse';

export const OnStartGame = async (
  isPlayerPlayWhite: boolean
): Promise<GameStartResponse> => {
  try {
    console.log(isPlayerPlayWhite);
    const token = localStorage.getItem('jwtToken');
    if (!token) throw new Error('Login again');

    const response = await axios.post(
      'http://localhost:5011/api/ChessMovement/OnGameStart',
      { isPlayerPlayWhite },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    console.log(response.data);
    return response.data;
  } catch (error: any) {
    if (axios.isAxiosError(error)) {
      if (error.response) {
        const errorMessage =
          error.response.data?.message ||
          'Something went wrong. Please try again or contact support.';
        throw new Error(errorMessage);
      }
    }
    throw error;
  }
};
