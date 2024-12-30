import { useState } from "react";
import Nav from "../../Main/Nav";
import { initialPieces } from "./GameScripts/Pieces";

const Computer = () => {
  const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
  const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

  const [peices, setPieces] = useState(initialPieces);
  const pieces = initialPieces;

  const getIndexFromPosition = (position: string): number => {
    const column = position[0]; // get a letter
    const row = position[1]; // get number

    const columnIndex = letters.indexOf(column);
    const rowIndex = numbers.indexOf(row) - 1;

    return rowIndex * 8 + columnIndex;
  };
  return (
    <>
      <Nav />
      <div className="flex-center max-height nav-padding">
        <div className="even-columns padding-block-400">
          <div className="chessboard-wrapper">
            <div className="numbers">
              {numbers.map((num) => (
                <div key={num} className="number">
                  {num}
                </div>
              ))}
            </div>
            <div className="chessboard">
              {[...Array(64)].map((_, i) => {
                const position = `${letters[i % 8]}${
                  numbers[Math.floor(i / 8)]
                }`;
                const piece = pieces.find((p) => p.position === position);

                return (
                  <div
                    key={i}
                    className={`cell ${
                      Math.floor(i / 8) % 2 === i % 2 ? "white" : "black"
                    }`}
                  >
                    {piece && (
                      <img
                        src={piece.svg}
                        alt={`${piece.color} ${piece.type}`}
                        className="chess-piece"
                      />
                    )}
                  </div>
                );
              })}
            </div>
            <div className="letters">
              {letters.map((letter) => (
                <div key={letter} className="letter">
                  {letter}
                </div>
              ))}
            </div>
          </div>
          <div className="game-info"></div>
        </div>
      </div>
    </>
  );
};

export default Computer;
