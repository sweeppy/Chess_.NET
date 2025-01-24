import { useState } from "react";
import Nav from "../../Main/Nav";
import { ChessPiece, initialPieces } from "./GameScripts/Pieces";
import { getLegalMoves } from "./GameScripts/LegalMoves";

const Computer = () => {
  const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
  const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

  const [pieces, setPieces] = useState(initialPieces);
  const [legalMoves, setLegalMoves] = useState<string[] | undefined>();
  const [selectedPiece, setSelectedPiece] = useState<ChessPiece | undefined>();

  const handlePieceClick = (piece: ChessPiece | undefined) => {
    if (piece) {
      setLegalMoves(getLegalMoves(piece, pieces, false));
      setSelectedPiece(piece);
    }
  };

  const clearPrevEnPassantable = (pieces: ChessPiece[]): ChessPiece[] => {
    const clearedPieces = pieces.map((p) => {
      if (p.type === "pawn") return { ...p, enPassantable: false };
      else return p;
    });
    return clearedPieces;
  };

  const handlePawnMove = (pawn: ChessPiece, newPosition: string): ChessPiece => {
    const moveDistance = Math.abs(+newPosition[1] - +pawn.position[1]);
    console.log(`pawn on ${pawn.position} have moveDistance: ${moveDistance}`);
    const isDoubleStep = moveDistance === 2;

    // if double step, enPassantable = true
    return { ...pawn, position: newPosition, enPassantable: isDoubleStep };
  };

  const getCastlingRookPositions = (
    king: ChessPiece,
    offset: number
  ): { oldPos: string; newPos: string } => {
    const isKingCastle = offset > 0;
    return isKingCastle
      ? { oldPos: `H${king.position[1]}`, newPos: `F${king.position[1]}` }
      : { oldPos: `A${king.position[1]}`, newPos: `D${king.position[1]}` };
  };

  const handleRookCastling = (
    oldPos: string,
    newPos: string,
    pieces: ChessPiece[]
  ): ChessPiece[] => {
    const updatedPieces = pieces.map((p) => {
      if (p.type === "rook" && p.position === oldPos) {
        return { ...p, position: newPos };
      }
      return p;
    });
    return updatedPieces;
  };

  const handleLegalMoveClick = (newPosition: string) => {
    // console.log("New Position:", newPosition);

    // clear all previous enPassantable
    let updatedPieces = clearPrevEnPassantable(pieces);

    let castlingRookPositions: { oldPos: string; newPos: string } | undefined;

    updatedPieces = updatedPieces.map((p) => {
      if (p.position === selectedPiece?.position) {
        console.log("asdf");
        if (selectedPiece.type === "pawn") {
          // return pawn on new position with enPassant option
          return handlePawnMove(p, newPosition);
        }

        // set 'hasMoves' for rook(important for castling)
        if (p.type === "rook")
          return { ...p, position: newPosition, hasMoved: true };

        // castling king logic
        if (p.type === "king") {
          // calculation offset of the king's position
          const offset =
            letters.indexOf(newPosition[0]) - letters.indexOf(p.position[0]);
          if (Math.abs(offset) > 1) {
            castlingRookPositions = getCastlingRookPositions(p, offset);
          }
          // add to king on new position 'hasMoved' = true
          else return { ...p, position: newPosition, hasMoved: true };
        }
        // default piece, that moved
        return { ...p, position: newPosition };
      }
      // not selected piece
      return p;
    });

    if (castlingRookPositions !== undefined) {
      updatedPieces = handleRookCastling(
        castlingRookPositions.oldPos,
        castlingRookPositions.newPos,
        updatedPieces
      );
    }

    setPieces(updatedPieces);

    setSelectedPiece(undefined);
    setLegalMoves(undefined);
  };

  return (
    <>
      <Nav />
      <div className="container nav-padding">
        <div className="even-columns padding-block-400 max-height">
          <div className="chessboard-wrapper">
            <div className="chessboard">
              {[...Array(64)].map((_, i) => {
                const position = `${letters[i % 8]}${numbers[Math.floor(i / 8)]}`;
                const piece = pieces.find((p) => p.position === position);

                return (
                  <div
                    key={i}
                    className={`cell ${
                      Math.floor(i / 8) % 2 === i % 2 ? "white" : "black"
                    }`}
                    onClick={() => handlePieceClick(piece)}
                  >
                    {i % 8 == 0 && (
                      <span
                        className={`cell-label row-label ${
                          Math.floor(i / 8) % 2 === i % 2 ? "black" : "white"
                        }`}
                      >
                        {numbers[Math.floor(i / 8)]}
                      </span>
                    )}
                    {i >= 56 && (
                      <span
                        className={`cell-label col-label ${
                          Math.floor(i / 8) % 2 === i % 2 ? "black" : "white"
                        }`}
                      >
                        {letters[i - 56]}
                      </span>
                    )}
                    {piece && (
                      <img
                        src={piece.svg}
                        alt={`${piece.color} ${piece.type}`}
                        className="chess-piece"
                      />
                    )}
                    {legalMoves?.includes(position) && (
                      <div
                        onClick={() => handleLegalMoveClick(position)}
                        className="container-lm"
                      >
                        <img
                          className="img-lm"
                          src={"/design/game/assets/moves/legal_move.svg"}
                          alt={"Legal move"}
                        />
                      </div>
                    )}
                  </div>
                );
              })}
            </div>
          </div>
          <div className="game-info"></div>
        </div>
      </div>
    </>
  );
};

export default Computer;
