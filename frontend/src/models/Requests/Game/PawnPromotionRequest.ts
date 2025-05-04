export interface PawnPromotionRequest {
  startSquare: number;
  targetSquare: number;
  fenBeforeMove: string;
  chosenPiece: string;
}
