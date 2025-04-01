export interface ChessPiece {
    type: 'p' | 'r' | 'n' | 'b' | 'q' | 'k' | 'P' | 'R' | 'N' | 'B' | 'Q' | 'K';
    squareIndex: number;
    svg: string;
    hasMoved?: boolean;
    enPassantable?: false | true;
}

export const initialPieces: ChessPiece[] = [
    // Black Pieces
    {
        type: 'r',
        squareIndex: 63,
        hasMoved: false,
        svg: '/src/assets/game/chess_pieces/B_Rook.svg',
    },
    {
        type: 'n',
        squareIndex: 62,
        svg: '/src/assets/game/chess_pieces/B_Knight.svg',
    },
    {
        type: 'b',
        squareIndex: 61,
        svg: '/src/assets/game/chess_pieces/B_Bishop.svg',
    },
    {
        type: 'q',
        squareIndex: 60,
        svg: '/src/assets/game/chess_pieces/B_Queen.svg',
    },
    {
        type: 'k',
        squareIndex: 59,
        hasMoved: false,
        svg: '/src/assets/game/chess_pieces/B_King.svg',
    },
    {
        type: 'b',
        squareIndex: 58,
        svg: '/src/assets/game/chess_pieces/B_Bishop.svg',
    },
    {
        type: 'n',
        squareIndex: 57,
        svg: '/src/assets/game/chess_pieces/B_Knight.svg',
    },
    {
        type: 'r',
        squareIndex: 56,
        hasMoved: false,
        svg: '/src/assets/game/chess_pieces/B_Rook.svg',
    },

    ...Array(8)
        .fill(null)
        .map(
            (_, i): ChessPiece => ({
                type: 'p',
                squareIndex: 48 + i,
                svg: '/src/assets/game/chess_pieces/B_Pawn.svg',
                enPassantable: false,
            })
        ),

    // White pieces
    {
        type: 'R',
        squareIndex: 7,
        hasMoved: false,
        svg: '/src/assets/game/chess_pieces/W_Rook.svg',
    },
    {
        type: 'N',
        squareIndex: 6,
        svg: '/src/assets/game/chess_pieces/W_Knight.svg',
    },
    {
        type: 'B',
        squareIndex: 5,
        svg: '/src/assets/game/chess_pieces/W_Bishop.svg',
    },
    {
        type: 'Q',
        squareIndex: 4,
        svg: '/src/assets/game/chess_pieces/W_Queen.svg',
    },
    {
        type: 'K',
        squareIndex: 3,
        hasMoved: false,
        svg: '/src/assets/game/chess_pieces/W_King.svg',
    },
    {
        type: 'B',
        squareIndex: 2,
        svg: '/src/assets/game/chess_pieces/W_Bishop.svg',
    },
    {
        type: 'N',
        squareIndex: 1,
        svg: '/src/assets/game/chess_pieces/W_Knight.svg',
    },
    {
        type: 'R',
        squareIndex: 0,
        hasMoved: false,
        svg: '/src/assets/game/chess_pieces/W_Rook.svg',
    },

    ...Array(8)
        .fill(null)
        .map(
            (_, i): ChessPiece => ({
                type: 'P',
                squareIndex: 8 + i,
                svg: '/src/assets/game/chess_pieces/W_Pawn.svg',
                enPassantable: false,
            })
        ),
];
