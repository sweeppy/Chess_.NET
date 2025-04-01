import { ChessPiece } from '../../../models/Game/Pieces';

export const GenerateFenFromBoard = (
    board: ChessPiece[],
    isWhiteTurn: boolean
) => {
    let fen = '';
    let emptyCount = 0;
    for (let rank = 7; rank >= 0; rank--) {
        for (let file = 7; file >= 0; file--) {
            const squareIndex = rank * 8 + file;
            const pieceSymbol = getPieceSymbolFromSquare(board, squareIndex);

            if (!pieceSymbol) emptyCount++;
            else {
                if (emptyCount > 0) {
                    fen += emptyCount.toString();
                    emptyCount = 0;
                }
                fen += pieceSymbol;
            }
        }

        if (emptyCount > 0) {
            fen += emptyCount.toString();
            emptyCount = 0;
        }
        if (rank > 0) fen += '/';
    }

    fen += isWhiteTurn ? ' w' : ' b';
};

const getPieceSymbolFromSquare = (board: ChessPiece[], squareIndex: number) => {
    board.forEach((piece) => {
        if ((piece.squareIndex = squareIndex)) return piece.type;
    });
    return undefined;
};
