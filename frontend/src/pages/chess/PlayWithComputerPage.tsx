import { useState } from "react";
import { getLegalMoves } from "../../components/Game/Play/GameScripts/LegalMoves";
import GameHistory from "../../components/chess/game/GameHistory";
import GameOptions from "../../components/chess/game/GameOptions";
import {
  ChessPiece,
  initialPieces,
} from "../../Components/Game/Play/GameScripts/Pieces";
import Nav from "../../components/chess/Nav";

const PlayWithComputerPage = () => {
  const letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
  const numbers = ["8", "7", "6", "5", "4", "3", "2", "1"];

  const [isGameStarted, setIsGameStarted] = useState(false);

  const [playerColor, setPlayerColor] = useState<"white" | "black">("white");
  const [playerTurn, setPlayerTurn] = useState<"white" | "black">("white");

  const [pieces, setPieces] = useState(initialPieces);
  const [legalMoves, setLegalMoves] = useState<string[] | undefined>();
  const [selectedPiece, setSelectedPiece] = useState<ChessPiece | undefined>();

  const handlePieceClick = (piece: ChessPiece | undefined) => {
    if (
      piece &&
      isGameStarted &&
      piece.color === playerColor &&
      playerColor === playerTurn
    ) {
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

  // get the new position for rook, while castling
  const getCastlingRookPositions = (
    king: ChessPiece,
    offset: number
  ): { oldPos: string; newPos: string } => {
    const isKingCastle = offset > 0;
    return isKingCastle
      ? { oldPos: `H${king.position[1]}`, newPos: `F${king.position[1]}` }
      : { oldPos: `A${king.position[1]}`, newPos: `D${king.position[1]}` };
  };

  // return pieces with new position for rook(rook after castling)
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

  // delete enemy piece and return remaining pieces
  const handleAttackPiece = (
    pieces: ChessPiece[],
    newPosition: string
  ): ChessPiece[] => {
    return pieces.filter((piece) => piece.position !== newPosition);
  };

  const handleLegalMoveClick = (newPosition: string) => {
    // console.log("New Position:", newPosition);

    // clear all previous enPassantable
    let updatedPieces = clearPrevEnPassantable(pieces);

    // click on square, where the enemy piece is located (delete enemy piece)
    updatedPieces = handleAttackPiece(updatedPieces, newPosition);

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
    setPlayerTurn(playerTurn === "white" ? "black" : "white");
  };

  // after game starts changing player color
  const handleColorChange = (color: "white" | "black") => {
    setPlayerColor(color);
  };

  const handleGameStart = () => {
    setIsGameStarted(true);
  };

  return (
    <>
      <Nav />
      <div className="container nav-padding">
        <div className="even-columns padding-block-400 max-height">
          <div className="chessboard-wrapper">
            <div className="chessboard">
              {[...Array(64)].map((_, i) => {
                const columnIndex = playerColor === "white" ? i % 8 : 7 - (i % 8);
                const rowIndex =
                  playerColor === "white"
                    ? Math.floor(i / 8)
                    : 7 - Math.floor(i / 8);
                const position = `${letters[columnIndex]}${numbers[rowIndex]}`;
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
                        {playerColor === "white"
                          ? numbers[Math.floor(i / 8)]
                          : numbers[Math.floor(7 - i / 8)]}
                      </span>
                    )}
                    {i >= 56 && (
                      <span
                        className={`cell-label col-label ${
                          Math.floor(i / 8) % 2 === i % 2 ? "black" : "white"
                        }`}
                      >
                        {playerColor === "white"
                          ? letters[i - 56]
                          : letters[63 - i]}
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
                          src={"/src/assets/game/moves/legal_move.svg"}
                          alt={"Legal move"}
                        />
                      </div>
                    )}
                  </div>
                );
              })}
            </div>
          </div>
          {isGameStarted ? (
            <GameHistory />
          ) : (
            <GameOptions
              OnColorChange={handleColorChange}
              OnGameStart={handleGameStart}
            />
          )}
        </div>
      </div>
    </>
  );
};

export default PlayWithComputerPage;
