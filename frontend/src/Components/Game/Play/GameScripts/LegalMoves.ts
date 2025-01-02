import { ChessPiece } from "./Pieces";

const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

const getLegalMoves = (
  piece: ChessPiece,
  alivePieces: ChessPiece[]
): string[] => {
  const columnIndex = letters.indexOf(piece.position[0]);
  const rowIndex = numbers.indexOf(piece.position[1]);
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
      return kingLegalMoves(piece, alivePieces);
      break;
    case "pawn":
      break;

    default:
      break;
  }
};

// Check the position(must be <=8 and <= H)
const isValidPosition = (columnIndex: number, rowIndex: number) => {
  return (
    letters.includes(letters[columnIndex]) &&
    numbers.includes(numbers[rowIndex])
  );
};

const kingLegalMoves = (
  king: ChessPiece,
  alivePieces: ChessPiece[]
): string[] => {
  const columnIdex = letters.indexOf(king.position[0]); // now column
  const rowIndex = numbers.indexOf(king.position[1]); // now row

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

  const probableMoves: string[] = []; // array with all king moves

  directions.forEach(([dx, dy]) => {
    const newColumnIndex = columnIdex + dx;
    const newRowIndex = rowIndex + dy;

    if (isValidPosition(newColumnIndex, newRowIndex)) {
      // create new position coordinates
      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      const pieceOnNewPosition = alivePieces.find(
        (piece) => piece.position === newPosition
      );

      if (!pieceOnNewPosition) {
        // no pieces on new position
        probableMoves.push(newPosition);
      } else if (pieceOnNewPosition.color !== king.color) {
        // king can capture the figure (will delete this move
        // later if it's under attack; check <<getAttackedSquares>> function
        // in the code below)
        probableMoves.push(newPosition);
      }
    }
  });
  // get all squares that are under attack
  const squaresUnderAttack = getAttackedSquares(alivePieces, king.color);

  // delete attacked squares from probableMoves
  const legalMoves = probableMoves.filter(
    (move) => !squaresUnderAttack.includes(move)
  );

  return legalMoves;
};

const getAttackedSquares = (
  alivePieces: ChessPiece[],
  color: "black" | "white" // color of the player, whose turn
): string[] => {
  const attackedSquares: string[] = [];

  alivePieces
    .filter((piece) => piece.color !== color)
    .forEach((piece) => {
      const pieceMoves = getLegalMoves(piece, alivePieces);

      attackedSquares.push(...pieceMoves);
    });
  return attackedSquares;
};
