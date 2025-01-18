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
      return getRookLegalMoves(piece, alivePieces);
      break;
    case "knight":
      return getKnightLegalMoves(piece, alivePieces);
      break;
    case "bishop":
      return getBishopLegalMoves(piece, alivePieces);
      break;
    case "queen":
      return getQueenLegalMoves(piece, alivePieces);
      break;
    case "king":
      return getKingLegalMoves(piece, alivePieces);
      break;
    case "pawn":
      return getPawnLegalMoves(piece, alivePieces);
      break;
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

      if (!isKingInSafe(rook.color, rook.position, newPosition, alivePieces))
        continue; //skip if the king will be unsafe

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

      // if king will be in safe and on new position and
      // there is no allied piece there
      if (
        isKingInSafe(knight.color, knight.position, newPosition, alivePieces) &&
        pieceOnNewPosition?.color !== knight.color
      ) {
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

      // if king will be unsafe after move
      if (!isKingInSafe(bishop.color, bishop.position, newPosition, AlivePieces))
        continue; // skip in while iterations

      const pieceOnNewPosition = AlivePieces.find((p) => {
        p.position === newPosition;
      });

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

      // if king will be unsafe after move
      if (!isKingInSafe(queen.color, queen.position, newPosition, alivePieces))
        continue; // skip this move

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
