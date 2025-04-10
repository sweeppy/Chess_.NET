import axios from 'axios';
import { MakeMoveRequest } from '../../../models/Requests/Game/MakeMoveRequest';
import { GameResponse } from '../../../models/Responses/Chess/GameStartResponse';

export const MakeMove = async (
  request: MakeMoveRequest
): Promise<GameResponse> => {
  try {
    const token = localStorage.getItem('jwtToken');
    if (!token) throw Error('Jwt token was not found');

    const response = await axios.post(
      'http://localhost:5011/api/ChessMovement/MakeMove',
      request,
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
