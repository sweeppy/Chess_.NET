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
      setLegalMoves(getLegalMoves(piece, pieces));
      setSelectedPiece(piece);
    }
  };

  const handleLegalMoveClick = (newPosition: string) => {
    console.log("New Position:", newPosition);

    setPieces((prevPieces) =>
      prevPieces.map((p) =>
        p === selectedPiece ? { ...p, position: newPosition } : p
      )
    );
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
