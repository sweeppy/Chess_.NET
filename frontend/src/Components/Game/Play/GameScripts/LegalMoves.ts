import { ChessPiece } from "./Pieces";

const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

export const getLegalMoves = (
  piece: ChessPiece,
  alivePieces: ChessPiece[],
  ignoreKingSafety: boolean
): string[] => {
  let legalMoves: string[] = [];
  switch (piece.type) {
    case "rook":
      legalMoves = getRookLegalMoves(piece, alivePieces);
      break;
    case "knight":
      legalMoves = getKnightLegalMoves(piece, alivePieces);
      break;
    case "bishop":
      legalMoves = getBishopLegalMoves(piece, alivePieces);
      break;
    case "queen":
      legalMoves = getQueenLegalMoves(piece, alivePieces);
      break;
    case "king":
      legalMoves = getKingLegalMoves(piece, alivePieces);
      break;
    case "pawn":
      legalMoves = getPawnLegalMoves(piece, alivePieces);
      break;
    default:
      return [];
  }

  // Check king safety only if ignoreKingSafety is false
  if (!ignoreKingSafety) {
    legalMoves = isKingInSafe(
      piece.color,
      piece.position,
      legalMoves,
      alivePieces
    );
  }
  return legalMoves;
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
      // create new position
      const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;

      const pieceOnNewPosition = alivePieces.find(
        (piece) => piece.position === newPosition
      );

      if (!pieceOnNewPosition) {
        // no pieces on new position
        legalMoves.push(newPosition);
      } else if (pieceOnNewPosition.color !== king.color) {
        legalMoves.push(newPosition);
      }
    }
  });

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
  for (const dy of forwardMoves) {
    const newRowIndex = oldRowIndex - dy * direction;
    const newPosition = `${letters[oldColumnIndex]}${numbers[newRowIndex]}`;
    const pieceOnNewPosition = AlivePieces.find(
      (p) => p.position === newPosition
    );
    if (!pieceOnNewPosition) legalMoves.push(newPosition);
    else break;
  }

  // attack moves
  const attackOffsets = [-1, 1];
  attackOffsets.forEach((dx) => {
    const newRowIndex = oldRowIndex - direction;
    const newColumnIndex = oldColumnIndex + dx;

    const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;
    const pieceOnNewPosition = AlivePieces.find(
      (p) => p.position === newPosition
    );
    if (pieceOnNewPosition) {
      if (pieceOnNewPosition?.color !== pawn.color) legalMoves.push(newPosition);
    }
  });

  // en passant moves
  attackOffsets.forEach((dx) => {
    const newRowIndex = oldRowIndex - direction;
    const newColumnIndex = oldColumnIndex + dx;

    const newPosition = `${letters[newColumnIndex]}${numbers[newRowIndex]}`;
    const enPassantPosition = `${letters[newColumnIndex]}${numbers[oldRowIndex]}`;

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
      const pieceMoves = getLegalMoves(piece, alivePieces, true);

      attackedSquares.push(...pieceMoves);
    });
  return attackedSquares;
};

// return string[] with final legal moves
const isKingInSafe = (
  playerColor: "white" | "black",
  oldPiecePosition: string,
  legalMoves: string[],
  alivePieces: ChessPiece[]
): string[] => {
  // moves that will show to player
  const finalLegalMoves: string[] = [];

  // check each move on king safety
  legalMoves.forEach((newPiecePosition) => {
    const simulatePieces = alivePieces.map((p) =>
      p.position === oldPiecePosition ? { ...p, position: newPiecePosition } : p
    );
    // get the player's king
    const king = simulatePieces.find(
      (p) => p.type === "king" && p.color === playerColor
    );

    if (!king) {
      throw new Error(`King of color ${playerColor} was not found.`);
    }
    // get attack squares if piece will be on new position
    const attackedSquares = getAttackedSquares(simulatePieces, playerColor);

    /* 
      if king will be in safe, push probable legal move to
      final_legal_moves array
    */
    if (!attackedSquares.includes(king.position))
      finalLegalMoves.push(newPiecePosition);
  });
  return finalLegalMoves;
};
