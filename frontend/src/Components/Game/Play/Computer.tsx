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

  const handleLegalMoveClick = (newPosition: string) => {
    console.log("New Position:", newPosition);

    let RookQueenCastleOptions = { oldRookPosition: "", newRookPosition: "" };
    let RookKingCastleOptions = { oldRookPosition: "", newRookPosition: "" };

    let updatedPieces = pieces.map((p) => {
      // clear all previous enPassantable
      if (p.type === "pawn" && p !== selectedPiece) {
        return { ...p, enPassantable: false };
      }

      if (p === selectedPiece) {
        if (selectedPiece.type === "pawn") {
          // if piece type = pawn, get distance and check double step
          const moveDistance = Math.abs(
            +newPosition[1] - +selectedPiece.position[1]
          );
          const isDoubleStep = moveDistance === 2;

          // if double step, enPassantable = true

          return { ...p, position: newPosition, enPassantable: isDoubleStep };
        }
        // set 'hasMoves' for rook(important for castling)
        if (p.type === "rook")
          return { ...p, position: newPosition, hasMoved: true };
        // castling king logic
        if (p.type === "king") {
          // calculation offset of the king's position
          const offset =
            letters.indexOf(newPosition[0]) - letters.indexOf(p.position[0]);
          // not castling move
          if (Math.abs(offset) !== 2) {
            return { ...p, position: newPosition, hasMoved: true };
          }
          // on which side castle
          if (offset > 0)
            RookKingCastleOptions = {
              oldRookPosition: `H${p.position[1]}`,
              newRookPosition: `F${p.position[1]}`,
            };
          else
            RookQueenCastleOptions = {
              oldRookPosition: `A${p.position[1]}`,
              newRookPosition: `D${p.position[1]}`,
            };
        }
        // default piece, that moved
        return { ...p, position: newPosition };
      }
      // not selected piece
      return p;
    });

    if (RookKingCastleOptions.oldRookPosition !== "") {
      updatedPieces = updatedPieces.map((p) => {
        if (
          p.type === "rook" &&
          p.position === RookKingCastleOptions.oldRookPosition
        )
          return { ...p, position: RookKingCastleOptions.newRookPosition };
        else return p;
      });
    } else if (RookQueenCastleOptions.oldRookPosition !== "") {
      updatedPieces = updatedPieces.map((p) => {
        if (
          p.type === "rook" &&
          p.position === RookQueenCastleOptions.oldRookPosition
        )
          return { ...p, position: RookQueenCastleOptions.newRookPosition };
        else return p;
      });
    }

    setPieces(updatedPieces);

    setSelectedPiece(undefined);
    setLegalMoves(undefined);
  };

  // const getIndexFromPosition = (position: string): number => {
  //   const column = position[0]; // get a letter
  //   const row = position[1]; // get number

  //   const columnIndex = letters.indexOf(column);
  //   const rowIndex = numbers.indexOf(row) - 1;

  //   return rowIndex * 8 + columnIndex;
  // };
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
