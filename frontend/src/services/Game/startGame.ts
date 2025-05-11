import axios from 'axios';
import { GameResponse } from '../../models/Responses/Chess/GameStartResponse';
import { chessClient } from '../apiClient';

export const OnStartGame = async (
  isPlayerPlayWhite: boolean
): Promise<GameResponse> => {
  try {
    const token = localStorage.getItem('jwtToken');
    if (!token) throw new Error('Login again');

    const response = await chessClient.post('/ChessMovement/OnGameStart', {
      isPlayerPlayWhite,
    });
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
