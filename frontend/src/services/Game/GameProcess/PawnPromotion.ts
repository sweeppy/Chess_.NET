import axios from 'axios';
import { PawnPromotionRequest } from '../../../models/Requests/Game/PawnPromotionRequest';
import { GameResponse } from '../../../models/Responses/Chess/GameStartResponse';

export const PromotePawn = async (
  request: PawnPromotionRequest
): Promise<GameResponse> => {
  try {
    const token = localStorage.getItem('jwtToken');
    if (!token) throw Error('Jwt token was not found');

    const response = await axios.post(
      'http://localhost:5011/api/ChessMovement/PromotePawn',
      request,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
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
