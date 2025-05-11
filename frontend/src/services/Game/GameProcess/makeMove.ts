import axios from 'axios';
import { MakeMoveRequest } from '../../../models/Requests/Game/MakeMoveRequest';
import { GameResponse } from '../../../models/Responses/Chess/GameStartResponse';
import { chessClient } from '../../apiClient';

export const MakeMove = async (
  request: MakeMoveRequest
): Promise<GameResponse> => {
  try {
    const token = localStorage.getItem('jwtToken');
    if (!token) throw Error('Jwt token was not found');

    const response = await chessClient.post('/ChessMovement/MakeMove', request);
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
