import { BaseResponse } from '../BaseResponse';

export interface GameStartResponse extends BaseResponse {
    fen: string;
    legalMoves: Record<number, number[]>;
}
