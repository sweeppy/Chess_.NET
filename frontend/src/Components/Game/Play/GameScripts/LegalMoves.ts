import { ChessPiece } from "./Pieces";

const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

export const getLegalMoves = (
  piece: ChessPiece,
  alivePieces: ChessPiece[]
): string[] => {
  switch (piece.type) {
    case "rook":
      return getRookLegalMoves(piece, alivePieces);

    case "knight":
      return getKnightLegalMoves(piece, alivePieces);

    case "bishop":
      return getBishopLegalMoves(piece, alivePieces);

    case "queen":
      return getQueenLegalMoves(piece, alivePieces);

    case "king":
      return getKingLegalMoves(piece, alivePieces);

    case "pawn":
      return getPawnLegalMoves(piece, alivePieces);
  }
};

// Check the position(must be <=8 and <= H)
const isValidPosition = (columnIndex: number, rowIndex: number): boolean => {
  return (
    letters.includes(letters[columnIndex]) && numbers.includes(numbers[rowIndex])
  );
};

// return string[] with legal moves for rook
const getRookLegalMoves = (
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

  for (const [dx, dy] of directions) {
    let newColumnIndex = columnIndex;
    let newRowIndex = rowIndex;

    while (true) {
      newColumnIndex += dx;
      newRowIndex += dy;

      if (!isValidPosition(newColumnIndex, newRowIndex)) break; // check indexes

      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      const pieceOnNewPosition = alivePieces.find(
        (piece) => piece.position === newPosition
      ); // get piece on new rook position (if exists)

      if (pieceOnNewPosition) {
        if (pieceOnNewPosition.color !== rook.color) {
          legalMoves.push(newPosition); // rook can take the piece
        }
        break;
      }
      legalMoves.push(newPosition);
    }
  }

  return legalMoves;
};
// return string[] with legal moves for rook
const getKnightLegalMoves = (
  knight: ChessPiece,
  alivePieces: ChessPiece[]
): string[] => {
  const legalMoves: string[] = [];
  const directions = [
    [1, 2],
    [1, -2],
    [-1, -2],
    [-1, 2],
    [2, 1],
    [2, -1],
    [-2, -1],
    [-2, 1],
  ];
  const oldColumnIndex = letters.indexOf(knight.position[0]);
  const oldRowIndex = numbers.indexOf(knight.position[1]);
  directions.forEach(([dx, dy]) => {
    const newColumnIndex = oldColumnIndex + dx;
    const newRowIndex = oldRowIndex + dy;
    if (isValidPosition(newColumnIndex, newRowIndex)) {
      // create new probable position
      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      // get piece on new position (if exists)
      const pieceOnNewPosition = alivePieces.find(
        (piece) => piece.position === newPosition
      );

      // if there is no allied piece there
      if (pieceOnNewPosition?.color !== knight.color) {
        legalMoves.push(newPosition);
      }
    }
  });
  return legalMoves;
};

// return string[] with legal moves for bishop
const getBishopLegalMoves = (
  bishop: ChessPiece,
  AlivePieces: ChessPiece[]
): string[] => {
  const legalMoves: string[] = [];

  const directions = [
    [1, 1],
    [1, -1],
    [-1, 1],
    [-1, -1],
  ];

  const oldColumnIndex = letters.indexOf(bishop.position[0]);
  const oldRowIndex = numbers.indexOf(bishop.position[1]);

  for (const [dx, dy] of directions) {
    let newColumnIndex = oldColumnIndex;
    let newRowIndex = oldRowIndex;
    while (true) {
      newColumnIndex += dx;
      newRowIndex += dy;

      // not valid square (8x8)
      if (!isValidPosition(newColumnIndex, newRowIndex)) break;

      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      const pieceOnNewPosition = AlivePieces.find(
        (p) => p.position === newPosition
      );

      if (pieceOnNewPosition) {
        if (pieceOnNewPosition.color !== bishop.color) {
          legalMoves.push(newPosition);
        }
        break;
      }

      legalMoves.push(newPosition);
    }
  }

  return legalMoves;
};

// return string[] with legal moves for queen
const getQueenLegalMoves = (
  queen: ChessPiece,
  alivePieces: ChessPiece[]
): string[] => {
  const legalMoves: string[] = [];

  const oldColumnIndex = letters.indexOf(queen.position[0]);
  const oldRowIndex = numbers.indexOf(queen.position[1]);

  const directions = [
    [1, 1],
    [1, -1],
    [-1, 1],
    [-1, -1],
    [0, 1], // up
    [0, -1], // down
    [1, 0], // right
    [-1, 0], // left
  ];

  for (const [dx, dy] of directions) {
    let newColumnIndex = oldColumnIndex;
    let newRowIndex = oldRowIndex;

    while (true) {
      newColumnIndex += dx;
      newRowIndex += dy;

      // invalid index (8x8)
      if (!isValidPosition(newColumnIndex, newRowIndex)) break;

      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      const pieceOnNewPosition = alivePieces.find(
        (p) => p.position === newPosition
      );

      if (pieceOnNewPosition) {
        if (pieceOnNewPosition.color !== queen.color) {
          legalMoves.push(newPosition);
        }
        break;
      }

      legalMoves.push(newPosition);
    }
  }

  return legalMoves;
};

// return string[] with legal moves for king
const getKingLegalMoves = (
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

// return string[] with legal moves for pawn
const getPawnLegalMoves = (
  pawn: ChessPiece,
  AlivePieces: ChessPiece[]
): string[] => {
  const legalMoves: string[] = [];

  const isWhite = pawn.color === "white" ? true : false;

  // which direction will the pawn move
  const direction = isWhite ? 1 : -1;
  const initialRow = isWhite ? "2" : "7";

  const oldColumnIndex = letters.indexOf(pawn.position[0]);
  const oldRowIndex = numbers.indexOf(pawn.position[1]);

  // forward moves
  const forwardMoves = [1];
  if (pawn.position[1] === initialRow) forwardMoves.push(2);
  forwardMoves.forEach((dy) => {
    const newRowIndex = oldRowIndex - dy * direction;
    const newPosition = `${letters[oldColumnIndex]}${numbers[newRowIndex]}`;
    const pieceOnNewPosition = AlivePieces.find(
      (p) => p.position === newPosition
    );
    if (!pieceOnNewPosition) legalMoves.push(newPosition);
  });

  // attack moves
  const attackOffsets = [-1, 1];
  attackOffsets.forEach((dx) => {
    const newRowIndex = oldRowIndex + direction;
    const newColumnIndex = oldColumnIndex + dx;

    const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;
    const pieceOnNewPosition = AlivePieces.find(
      (p) => p.position === newPosition
    );

    if (pieceOnNewPosition?.color !== pawn.color) legalMoves.push(newPosition);
  });

  // en passant moves
  attackOffsets.forEach((dx) => {
    const newRowIndex = oldRowIndex + direction;
    const newColumnIndex = oldColumnIndex + dx;

    const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;
    const enPassantPosition = `${letters[oldColumnIndex]}${numbers[newRowIndex]}`;
    AlivePieces.forEach((p) => {
      if (p.position === enPassantPosition && p.enPassantable === true) {
        legalMoves.push(newPosition);
      }
    });
  });

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
  console.log("Checking king safety:", {
    playerColor,
    oldPiecePosition,
    newPiecePosition,
  });

  const simulatePieces = alivePieces.map((p) =>
    p.position === oldPiecePosition ? { ...p, position: newPiecePosition } : p
  );

  const king = simulatePieces.find(
    (p) => p.type === "king" && p.color === playerColor
  );

  if (!king) {
    throw new Error(`King of color ${playerColor} was not found.`);
  }

  const otherPieces = simulatePieces.filter(
    (p) => p.position !== newPiecePosition
  );

  const attackedSquares = getAttackedSquares(otherPieces, king.color);

  console.log("Attacked squares:", attackedSquares);

  return !attackedSquares.includes(king.position);
};
