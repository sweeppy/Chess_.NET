import { ChessPiece } from '../../../models/Game/Pieces';

export const getPiecesFromFen = (fen: string) => {
  const pieces: ChessPiece[] = [];
  const fenParts = fen.split(' ');
  const boardPart = fenParts[0];

  let rank = 7;
  let file = 0;

  for (const symbol of boardPart) {
    if (symbol === '/') {
      rank--;
      file = 0;
    } else if (/\d/.test(symbol)) {
      file += parseInt(symbol, 10);
    } else {
      const squareIndex = rank * 8 + (7 - file);
      const type = symbol as ChessPiece['type'];

      pieces.push({
        type,
        squareIndex,
        svg: getPieceSvg(type),
      });
      file++;
    }
  }
  return pieces;
};

const getPieceSvg = (type: ChessPiece['type']) => {
  const pieceMap = {
    p: '/src/assets/game/chess_pieces/B_Pawn.svg',
    r: '/src/assets/game/chess_pieces/B_Rook.svg',
    n: '/src/assets/game/chess_pieces/B_Knight.svg',
    b: '/src/assets/game/chess_pieces/B_Bishop.svg',
    q: '/src/assets/game/chess_pieces/B_Queen.svg',
    k: '/src/assets/game/chess_pieces/B_King.svg',
    P: '/src/assets/game/chess_pieces/W_Pawn.svg',
    R: '/src/assets/game/chess_pieces/W_Rook.svg',
    N: '/src/assets/game/chess_pieces/W_Knight.svg',
    B: '/src/assets/game/chess_pieces/W_Bishop.svg',
    Q: '/src/assets/game/chess_pieces/W_Queen.svg',
    K: '/src/assets/game/chess_pieces/W_King.svg',
  };
  return pieceMap[type];
};

// const getPieceSymbolFromSquare = (board: ChessPiece[], squareIndex: number) => {
//   board.forEach((piece) => {
//     if ((piece.squareIndex = squareIndex)) return piece.type;
//   });
//   return undefined;
// };
