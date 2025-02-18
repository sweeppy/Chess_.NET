export interface ChessPiece {
  type: "rook" | "knight" | "bishop" | "queen" | "king" | "pawn";
  color: "black" | "white";
  position: string;
  svg: string;
  hasMoved?: boolean;
  enPassantable?: false | true;
}

export const initialPieces: ChessPiece[] = [
  // Black Pieces
  {
    type: "rook",
    color: "black",
    position: "A8",
    hasMoved: false,
    svg: "/src/assets/game/chess_pieces/B_Rook.svg",
  },
  {
    type: "knight",
    color: "black",
    position: "B8",
    svg: "/src/assets/game/chess_pieces/B_Knight.svg",
  },
  {
    type: "bishop",
    color: "black",
    position: "C8",
    svg: "/src/assets/game/chess_pieces/B_Bishop.svg",
  },
  {
    type: "queen",
    color: "black",
    position: "D8",
    svg: "/src/assets/game/chess_pieces/B_Queen.svg",
  },
  {
    type: "king",
    color: "black",
    position: "E8",
    hasMoved: false,
    svg: "/src/assets/game/chess_pieces/B_King.svg",
  },
  {
    type: "bishop",
    color: "black",
    position: "F8",
    svg: "/src/assets/game/chess_pieces/B_Bishop.svg",
  },
  {
    type: "knight",
    color: "black",
    position: "G8",
    svg: "/src/assets/game/chess_pieces/B_Knight.svg",
  },
  {
    type: "rook",
    color: "black",
    position: "H8",
    hasMoved: false,
    svg: "/src/assets/game/chess_pieces/B_Rook.svg",
  },

  ...Array(8)
    .fill(null)
    .map(
      (_, i): ChessPiece => ({
        type: "pawn",
        color: "black",
        position: `${String.fromCharCode(65 + i)}7`,
        svg: "/src/assets/game/chess_pieces/B_Pawn.svg",
        enPassantable: false,
      })
    ),

  // White pieces
  {
    type: "rook",
    color: "white",
    position: "A1",
    hasMoved: false,
    svg: "/src/assets/game/chess_pieces/W_Rook.svg",
  },
  {
    type: "knight",
    color: "white",
    position: "B1",
    svg: "/src/assets/game/chess_pieces/W_Knight.svg",
  },
  {
    type: "bishop",
    color: "white",
    position: "C1",
    svg: "/src/assets/game/chess_pieces/W_Bishop.svg",
  },
  {
    type: "queen",
    color: "white",
    position: "D1",
    svg: "/src/assets/game/chess_pieces/W_Queen.svg",
  },
  {
    type: "king",
    color: "white",
    position: "E1",
    hasMoved: false,
    svg: "/src/assets/game/chess_pieces/W_King.svg",
  },
  {
    type: "bishop",
    color: "white",
    position: "F1",
    svg: "/src/assets/game/chess_pieces/W_Bishop.svg",
  },
  {
    type: "knight",
    color: "white",
    position: "G1",
    svg: "/src/assets/game/chess_pieces/W_Knight.svg",
  },
  {
    type: "rook",
    color: "white",
    position: "H1",
    hasMoved: false,
    svg: "/src/assets/game/chess_pieces/W_Rook.svg",
  },

  ...Array(8)
    .fill(null)
    .map(
      (_, i): ChessPiece => ({
        type: "pawn",
        color: "white",
        position: `${String.fromCharCode(65 + i)}2`,
        svg: "/src/assets/game/chess_pieces/W_Pawn.svg",
        enPassantable: false,
      })
    ),
];
