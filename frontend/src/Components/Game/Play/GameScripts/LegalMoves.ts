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
      return rookLegalMoves(piece, alivePieces);
      break;
    case "knight":
      return kingLegalMoves(piece, alivePieces);
      break;
    case "bishop":
      return kingLegalMoves(piece, alivePieces);
      break;
    case "queen":
      return kingLegalMoves(piece, alivePieces);
      break;
    case "king":
      return kingLegalMoves(piece, alivePieces);
      break;
    case "pawn":
      return kingLegalMoves(piece, alivePieces);
      break;
  }
};

// Check the position(must be <=8 and <= H)
const isValidPosition = (columnIndex: number, rowIndex: number): boolean => {
  return (
    letters.includes(letters[columnIndex]) &&
    numbers.includes(numbers[rowIndex])
  );
};

// return string[] with legal moves for rook
const rookLegalMoves = (
  rook: ChessPiece,
  alivePieces: ChessPiece[]
): string[] => {
  const columnIndex = letters.indexOf(rook.position[0]);
  const rowIndex = numbers.indexOf(rook.position[1]);

  const directions = [
    [0, 1], // up
    [0, -1], // down
    [1, 0], // right
    [-1, 0], // left
  ];
  const legalMoves: string[] = [];

  directions.forEach(([dx, dy]) => {
    let newColumnIndex = columnIndex;
    let newRowIndex = rowIndex;

    while (true) {
      newColumnIndex += dx;
      newRowIndex += dy;

      if (!isValidPosition(newColumnIndex, newRowIndex)) break; // check indexes

      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      if (!isKingInSafe(rook.color, rook.position, newPosition, alivePieces))
        break; // if king will not in safe after THIS move

      const pieceOnNewPosition = alivePieces.find(
        (piece) => piece.position === newPosition
      ); // get piece on new rook position (if exists)

      if (pieceOnNewPosition) {
        if (pieceOnNewPosition.color !== rook.color)
          legalMoves.push(newPosition); // rook can take the piece
        else break;
      } else legalMoves.push(newPosition);
    }
  });

  return legalMoves;
};

// return string[] with legal moves for king
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

// return string[] with attacked positions
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

// return boolean, that shows will be king in safe
// after move by selected piece or not
const isKingInSafe = (
  playerColor: string,
  oldPiecePosition: string,
  newPiecePosition: string,
  alivePieces: ChessPiece[]
): boolean => {
  const simulatePieces = alivePieces.map((p) =>
    // move selected piece
    p.position === oldPiecePosition ? { ...p, position: newPiecePosition } : p
  );
  const king = simulatePieces.find((p) => {
    p.type === "king" && p.color === playerColor;
  });
  if (!king) {
    console.error("An error occurred, while calculating the king's safety.");
    throw new Error(`King of color ${playerColor} was not found.`);
  }

  const attackedSquares = getAttackedSquares(simulatePieces, king.color);

  return !attackedSquares.includes(king.position);
};
