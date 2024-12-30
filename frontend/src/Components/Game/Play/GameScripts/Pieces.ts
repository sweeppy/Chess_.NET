export interface ChessPiece {
  type: string;
  color: string;
  position: string;
  svg: string;
}

export const initialPieces: ChessPiece[] = [
  // Black Pieces
  {
    type: "rook",
    color: "black",
    position: "A8",
    svg: "/design/game/assets/chess_pieces/B_Rook.svg",
  },
  {
    type: "knight",
    color: "black",
    position: "B8",
    svg: "/design/game/assets/chess_pieces/B_Knight.svg",
  },
  {
    type: "bishop",
    color: "black",
    position: "C8",
    svg: "/design/game/assets/chess_pieces/B_Bishop.svg",
  },
  {
    type: "queen",
    color: "black",
    position: "D8",
    svg: "/design/game/assets/chess_pieces/B_Queen.svg",
  },
  {
    type: "king",
    color: "black",
    position: "E8",
    svg: "/design/game/assets/chess_pieces/B_King.svg",
  },
  {
    type: "bishop",
    color: "black",
    position: "F8",
    svg: "/design/game/assets/chess_pieces/B_Bishop.svg",
  },
  {
    type: "knight",
    color: "black",
    position: "G8",
    svg: "/design/game/assets/chess_pieces/B_Knight.svg",
  },
  {
    type: "rook",
    color: "black",
    position: "H8",
    svg: "/design/game/assets/chess_pieces/B_Rook.svg",
  },

  ...Array(8)
    .fill(null)
    .map((_, i) => ({
      type: "pawn",
      color: "black",
      position: `${String.fromCharCode(65 + i)}7`,
      svg: "/design/game/assets/chess_pieces/B_Pawn.svg",
    })),

  // White pieces
  {
    type: "rook",
    color: "white",
    position: "A1",
    svg: "/design/game/assets/chess_pieces/W_Rook.svg",
  },
  {
    type: "knight",
    color: "white",
    position: "B1",
    svg: "/design/game/assets/chess_pieces/W_Knight.svg",
  },
  {
    type: "bishop",
    color: "white",
    position: "C1",
    svg: "/design/game/assets/chess_pieces/W_Bishop.svg",
  },
  {
    type: "queen",
    color: "white",
    position: "D1",
    svg: "/design/game/assets/chess_pieces/W_Queen.svg",
  },
  {
    type: "king",
    color: "white",
    position: "E1",
    svg: "/design/game/assets/chess_pieces/W_King.svg",
  },
  {
    type: "bishop",
    color: "white",
    position: "F1",
    svg: "/design/game/assets/chess_pieces/W_Bishop.svg",
  },
  {
    type: "knight",
    color: "white",
    position: "G1",
    svg: "/design/game/assets/chess_pieces/W_Knight.svg",
  },
  {
    type: "rook",
    color: "white",
    position: "H1",
    svg: "/design/game/assets/chess_pieces/W_Rook.svg",
  },

  ...Array(8)
    .fill(null)
    .map((_, i) => ({
      type: "pawn",
      color: "white",
      position: `${String.fromCharCode(65 + i)}2`,
      svg: "/design/game/assets/chess_pieces/W_Pawn.svg",
    })),
];
