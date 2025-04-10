import { BaseResponse } from '../BaseResponse';

export interface GameResponse extends BaseResponse {
  fen: string;
  legalMoves: Record<number, number[]>;
}
