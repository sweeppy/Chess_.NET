import axios from 'axios';
import { PawnPromotionRequest } from '../../../models/Requests/Game/PawnPromotionRequest';
import { GameResponse } from '../../../models/Responses/Chess/GameStartResponse';
import { chessClient } from '../../apiClient';

export const PromotePawn = async (
  request: PawnPromotionRequest
): Promise<GameResponse> => {
  try {
    const token = localStorage.getItem('jwtToken');
    if (!token) throw Error('Jwt token was not found');

    const response = await chessClient.post(
      '/ChessMovement/PromotePawn',
      request
    );
    return response.data;
  } catch (error) {
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
