import { useState } from 'react';

import GameHistory from '../../components/chess/pages/play/GameHistory';
import GameOptions from '../../components/chess/pages/play/GameOptions';
import Nav from '../../components/chess/Nav';

import ErrorAlert from '../../components/alerts/ErrorAlert';
import { ChessPiece, initialPieces } from '../../models/Game/Pieces';
import { OnStartGame } from '../../services/Game/startGame';
import { getPiecesFromFen } from '../../services/Game/GameProcess/fenUtility';
import { MakeMove } from '../../services/Game/GameProcess/makeMove';
import { MakeMoveRequest } from '../../models/Requests/Game/MakeMoveRequest';
import Winner from '../../components/chess/pages/play/Winner';
import Promotion from '../../components/chess/pages/play/Promotion';
import { PromotePawn } from '../../services/Game/GameProcess/PawnPromotion';
import { PawnPromotionRequest } from '../../models/Requests/Game/PawnPromotionRequest';

const PlayWithComputerPage = () => {
  const letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];
  const numbers = ['8', '7', '6', '5', '4', '3', '2', '1'];

  const [isGameStarted, setIsGameStarted] = useState(false);

  const [pieces, setPieces] = useState<ChessPiece[]>(initialPieces);
  const [allLegalMoves, setAllLegalMoves] = useState<Record<
    number,
    number[]
  > | null>(null);
  const [currentLegalMoves, setCurrentLegalMoves] = useState<
    number[] | undefined
  >(undefined);
  const [currentFen, setCurrentFen] = useState<string>();

  const [playerColor, setPlayerColor] = useState<'white' | 'black'>('white');

  const [errorAlertMessage, setErrorAlertMessage] = useState<string | null>(
    null
  );
  const [isErrorAlertClosing, setIsErrorAlertClosing] = useState(false);

  const [startSquare, setStartSquare] = useState<number | null>(null);

  const [moveNotations, setMoveNotations] = useState<string[] | null>(null);

  const [winner, setWinner] = useState<string | null>(null);

  const [promotionData, setPromotionData] = useState<{
    isOpen: boolean;
    squareIndex: number | null;
  }>({ isOpen: false, squareIndex: null });

  const handlePieceClick = (squareIndex: number) => {
    setCurrentLegalMoves(allLegalMoves?.[squareIndex]);
    setStartSquare(squareIndex);
  };

  const handlePieceMove = async (targetSquare: number) => {
    try {
      if (startSquare != undefined && targetSquare != undefined && currentFen) {
        const piece = pieces.find((p) => p.squareIndex === startSquare);

        if (piece && (piece.type === 'P' || piece.type === 'p')) {
          const isWhitePromotion = piece.type === 'P' && targetSquare >= 56;
          const isBlackPromotion = piece.type === 'p' && targetSquare <= 7;

          if (isWhitePromotion || isBlackPromotion) {
            setPromotionData({ isOpen: true, squareIndex: targetSquare });
            return;
          }
        }

        const request: MakeMoveRequest = {
          startSquare: startSquare,
          targetSquare: targetSquare,
          fenBeforeMove: currentFen,
        };

        const response = await MakeMove(request);

        if (response.isGameEnded) {
          setWinner(response.winner);
        }

        setPieces(getPiecesFromFen(response.fen));
        setAllLegalMoves(response.legalMoves);
        setCurrentLegalMoves(undefined);
        setCurrentFen(response.fen);
        console.log(response);
        setMoveNotations(response.moveNotations);
      }
    } catch (error: any) {
      setErrorAlertMessage(error.message);
    }
  };

  const handleGameStart = async (color: 'white' | 'black') => {
    try {
      const response = await OnStartGame(color === 'white');
      setCurrentFen(response.fen);
      setAllLegalMoves(response.legalMoves);
      setIsGameStarted(true);
      setPieces(getPiecesFromFen(response.fen));
      setMoveNotations(response.moveNotations);
    } catch (error: any) {
      setErrorAlertMessage(error.message);
    }
  };

  const handleColorChange = (color: 'white' | 'black') => {
    setPlayerColor(color);
  };

  const isAlliedPiece = (piece?: ChessPiece) => {
    if (!piece) return false;
    return (
      (piece.type === piece.type.toLocaleLowerCase() &&
        playerColor == 'black') ||
      (piece.type === piece.type.toUpperCase() && playerColor == 'white')
    );
  };

  const handleErrorAlertClose = () => {
    setIsErrorAlertClosing(true);
    setTimeout(() => {
      setErrorAlertMessage(null);
      setIsErrorAlertClosing(false);
    }, 500);
  };
  const handlePromotionSelect = async (pieceType: string) => {
    if (
      promotionData.squareIndex !== null &&
      startSquare !== null &&
      currentFen
    ) {
      const promoteRequest: PawnPromotionRequest = {
        startSquare: startSquare,
        targetSquare: promotionData.squareIndex,
        fenBeforeMove: currentFen,
        chosenPiece: pieceType,
      };
      const data = await PromotePawn(promoteRequest);
      if (data.isGameEnded) {
        setWinner(data.winner);
      }

      setCurrentFen(data.fen);
      setMoveNotations(data.moveNotations);
      setAllLegalMoves(data.legalMoves);
      setCurrentLegalMoves(undefined);
      setPieces(getPiecesFromFen(data.fen));

      setPromotionData({ isOpen: false, squareIndex: null });
    }
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
      <Promotion
        isOpen={promotionData.isOpen}
        playerColor={playerColor}
        onSelect={handlePromotionSelect}
        onClose={() => setPromotionData({ isOpen: false, squareIndex: null })}
      />
      <Nav />
      <div className="container nav-padding">
        <div className="even-columns padding-block-400 max-height">
          <div>
            {winner && <Winner winnerName={winner} />}
            <div className="chessboard-wrapper">
              <div className="chessboard">
                {[...Array(64)].map((_, i) => {
                  const squareIndex = playerColor == 'white' ? 63 - i : i;
                  const piece = pieces.find(
                    (p) => p.squareIndex === squareIndex
                  );

                  return (
                    <div
                      key={i}
                      className={`cell ${
                        Math.floor(i / 8) % 2 === i % 2 ? 'white' : 'black'
                      }`}
                      onClick={
                        isAlliedPiece(piece)
                          ? () => handlePieceClick(squareIndex)
                          : () => {}
                      }
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
                          className={`chess-piece ${
                            isAlliedPiece(piece) ? 'allied' : ''
                          }`}
                        />
                      )}
                      {currentLegalMoves?.includes(squareIndex) && (
                        <div
                          onClick={() => handlePieceMove(squareIndex)}
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
          </div>

          {isGameStarted ? (
            <>
              <GameHistory moveNotations={moveNotations} />
            </>
          ) : (
            <GameOptions
              OnGameStart={handleGameStart}
              OnColorChange={handleColorChange}
            />
          )}
        </div>
      </div>
    </>
  );
};

export default PlayWithComputerPage;
