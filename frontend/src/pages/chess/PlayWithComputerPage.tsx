import { useState } from 'react';

import GameHistory from '../../components/chess/game/GameHistory';
import GameOptions from '../../components/chess/game/GameOptions';
import Nav from '../../components/chess/Nav';

import ErrorAlert from '../../components/alerts/ErrorAlert';
import { ChessPiece, initialPieces } from '../../models/Game/Pieces';
import { OnStartGame } from '../../services/Game/startGame';

const PlayWithComputerPage = () => {
  const letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];
  const numbers = ['8', '7', '6', '5', '4', '3', '2', '1'];

  const [isGameStarted, setIsGameStarted] = useState(false);

  const [pieces, setPieces] = useState(initialPieces);
  const [allLegalMoves, setAllLegalMoves] = useState<Record<
    number,
    number[]
  > | null>(null);
  const [currentLegalMoves, setCurrentLegalMoves] = useState<
    number[] | undefined
  >(undefined);
  const [currentFen, setCurrentFen] = useState<string>();
  const [selectedPiece, setSelectedPiece] = useState<ChessPiece | undefined>();

  const [playerColor, setPlayerColor] = useState<'white' | 'black'>('white');

  const [errorAlertMessage, setErrorAlertMessage] = useState<string | null>(
    null
  );
  const [isErrorAlertClosing, setIsErrorAlertClosing] = useState(false);

  const handlePieceClick = (squareIndex: number) => {
    setCurrentLegalMoves(allLegalMoves?.[squareIndex]);
  };

  const handlePieceMove = () => {};

  const handleLegalMoveClick = (newSquareIndex: number) => {};

  const handleGameStart = async () => {
    try {
      const response = await OnStartGame();
      console.log(response.fen);
      console.log(response.legalMoves);
      setCurrentFen(response.fen);
      setAllLegalMoves(response.legalMoves);
      setIsGameStarted(true);
    } catch (error: any) {
      setErrorAlertMessage(error.message);
    }
  };

  const handleColorChange = (color: 'white' | 'black') => {
    setPlayerColor(color);
  };

  const handleErrorAlertClose = () => {
    setIsErrorAlertClosing(true);
    setTimeout(() => {
      setErrorAlertMessage(null);
      setIsErrorAlertClosing(false);
    }, 500);
  };

  return (
    <>
      {errorAlertMessage && (
        <ErrorAlert
          isAlertClosing={isErrorAlertClosing}
          errorMessage={errorAlertMessage}
          closeAlert={handleErrorAlertClose}
        />
      )}
      <Nav />
      <div className="container nav-padding">
        <div className="even-columns padding-block-400 max-height">
          <div className="chessboard-wrapper">
            <div className="chessboard">
              {[...Array(64)].map((_, i) => {
                const squareIndex = 63 - i;
                const piece = pieces.find((p) => p.squareIndex === squareIndex);

                return (
                  <div
                    key={i}
                    className={`cell ${
                      Math.floor(i / 8) % 2 === i % 2 ? 'white' : 'black'
                    }`}
                    onClick={() => handlePieceClick(squareIndex)}
                  >
                    {i % 8 == 0 && (
                      <span
                        className={`cell-label row-label ${
                          Math.floor(i / 8) % 2 === i % 2 ? 'black' : 'white'
                        }`}
                      >
                        {playerColor === 'white'
                          ? numbers[Math.floor(i / 8)]
                          : numbers[Math.floor(7 - i / 8)]}
                      </span>
                    )}
                    {i >= 56 && (
                      <span
                        className={`cell-label col-label ${
                          Math.floor(i / 8) % 2 === i % 2 ? 'black' : 'white'
                        }`}
                      >
                        {playerColor === 'white'
                          ? letters[i - 56]
                          : letters[63 - i]}
                      </span>
                    )}
                    {piece && (
                      <img
                        src={piece.svg}
                        alt={`${piece.type}`}
                        className="chess-piece"
                      />
                    )}
                    {currentLegalMoves?.includes(squareIndex) && (
                      <div
                        onClick={() => handleLegalMoveClick(squareIndex)}
                        className="container-lm"
                      >
                        <img
                          className="img-lm"
                          src={'/src/assets/game/moves/legal_move.svg'}
                          alt={'Legal move'}
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
