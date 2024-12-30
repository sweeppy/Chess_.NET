import { ChessPiece } from "./Pieces";
const showLegalMoves = (piece: ChessPiece, alivePieces: ChessPiece[]) => {
  const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
  const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

  const currentColumn = piece.position[0];
  const currentRow = piece.position[1];
  const columnIndex = letters.indexOf(currentColumn);
  const rowIndex = numbers.indexOf(currentRow);
  switch (piece.type) {
    case "rook":
      break;

    default:
      break;
  }
};
