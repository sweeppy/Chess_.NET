import { ChessPiece } from "./Pieces";

const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

const showLegalMoves = (piece: ChessPiece, alivePieces: ChessPiece[]) => {
  const currentColumn = piece.position[0];
  const currentRow = piece.position[1];
  const columnIndex = letters.indexOf(currentColumn);
  const rowIndex = numbers.indexOf(currentRow);
  switch (piece.type) {
    case "rook":
      break;
    case "knight":
      break;
    case "bishop":
      break;
    case "queen":
      break;
    case "king":
      break;
    case "pawn":
      break;

    default:
      break;
  }
};

const isValidPosition = (columnIndex: number, rowIndex: number) => {
  return (
    letters.includes(letters[columnIndex]) &&
    numbers.includes(numbers[rowIndex])
  );
};

const kingMoves = (position: string): string[] => {
  const columnIdex = letters.indexOf(position[0]); // now column
  const rowIndex = numbers.indexOf(position[1]); // now row

  const directions = [
    // First number - column, second - row

    [-1, -1],
    [-1, 0],
    [-1, 1],
    [0, -1],
    [0, 1],
    [1, -1],
    [1, 0],
    [1, 1],
  ]; // all squares around the king

  const legalMoves: string[] = []; // array with all king moves

  directions.forEach(([dx, dy]) => {
    const newColumnIndex = columnIdex + dx;
    const newRowIndex = rowIndex + dy;

    if (isValidPosition(newColumnIndex, newRowIndex)) {
      // if new position legal, add it to array "legalMoves"
      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;
      legalMoves.push(newPosition);
    }
  });

  return legalMoves;
};

const getAttackedSquares = (
  alivePieces: ChessPiece[],
  color: "black" | "white"
): string[] => {
  const getAttackedSquares: string[] = [];

  alivePieces.filter((piece) => piece.color !== color).forEach((piece) => {});
};
