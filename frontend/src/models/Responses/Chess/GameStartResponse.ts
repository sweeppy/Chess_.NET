import { BaseResponse } from '../BaseResponse';

export interface GameResponse extends BaseResponse {
  fen: string;
  legalMoves: Record<number, number[]>;
  moveNotations: string[] | null;
  isGameEnded: boolean;
  winner: string | null;
}
