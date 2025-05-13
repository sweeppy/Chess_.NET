import { useState, useRef } from 'react';

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
  const [isProcessingMove, setIsProcessingMove] = useState(false);

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

  // Add a ref to store the last valid board state
  const lastValidBoardState = useRef<{
    pieces: ChessPiece[];
    fen: string | undefined;
  }>({
    pieces: initialPieces,
    fen: undefined,
  });

  const handlePieceClick = (squareIndex: number) => {
    if (isProcessingMove) return; // Prevent new moves while processing
    setCurrentLegalMoves(allLegalMoves?.[squareIndex]);
    setStartSquare(squareIndex);
  };

  const updatePiecePosition = (from: number, to: number) => {
    setPieces((prevPieces) => {
      const newPieces = prevPieces.map((piece) => ({ ...piece })); // Deep copy
      const movingPiece = newPieces.find((p) => p.squareIndex === from);

      if (!movingPiece) return prevPieces;

      // Handle castling
      if (movingPiece.type.toLowerCase() === 'k') {
        const rank = Math.floor(from / 8) * 8; // Get the rank (0 or 56 for kings)
        // Queen-side castling
        if (to - from === 2) {
          const rook = newPieces.find((p) => p.squareIndex === rank + 7); // h1 or h8
          if (rook) {
            rook.squareIndex = rank + 4; // d1 or d8
          }
        }
        // King-side castling
        else if (from - to === 2) {
          const rook = newPieces.find((p) => p.squareIndex === rank); // a1 or a8
          if (rook) {
            rook.squareIndex = rank + 2; // f1 or f8
          }
        }
      }

      // Handle en passant for pawns
      if (movingPiece.type.toLowerCase() === 'p') {
        const isCaptureDiagonal = Math.abs((from % 8) - (to % 8)) === 1;
        if (isCaptureDiagonal) {
          // Remove captured pawn in en passant
          const capturedPawnSquare = from + (to > from ? 1 : -1);
          const capturedPawnIndex = newPieces.findIndex(
            (p) =>
              p.squareIndex === capturedPawnSquare &&
              p.type.toLowerCase() === 'p'
          );
          if (capturedPawnIndex !== -1) {
            newPieces.splice(capturedPawnIndex, 1);
          }
        }
      }

      // Remove captured piece if any
      const capturedPieceIndex = newPieces.findIndex(
        (p) => p.squareIndex === to
      );
      if (capturedPieceIndex !== -1) {
        newPieces.splice(capturedPieceIndex, 1);
      }

      // Move the piece
      movingPiece.squareIndex = to;

      return newPieces;
    });
  };

  const handlePieceMove = async (targetSquare: number) => {
    try {
      if (startSquare != undefined && targetSquare != undefined && currentFen) {
        const piece = pieces.find((p) => p.squareIndex === startSquare);

        // Validate that this is a legal move
        if (!piece || !currentLegalMoves?.includes(targetSquare)) {
          return;
        }

        if (piece && (piece.type === 'P' || piece.type === 'p')) {
          const isWhitePromotion = piece.type === 'P' && targetSquare >= 56;
          const isBlackPromotion = piece.type === 'p' && targetSquare <= 7;

          if (isWhitePromotion || isBlackPromotion) {
            setPromotionData({ isOpen: true, squareIndex: targetSquare });
            return;
          }
        }

        // Save current state before making changes
        lastValidBoardState.current = {
          pieces: pieces.map((piece) => ({ ...piece })),
          fen: currentFen,
        };

        // Show immediate visual feedback only for legal moves
        updatePiecePosition(startSquare, targetSquare);

        // Set processing state
        setIsProcessingMove(true);
        setCurrentLegalMoves(undefined);
        setStartSquare(null);

        const request: MakeMoveRequest = {
          startSquare: startSquare,
          targetSquare: targetSquare,
          fenBeforeMove: currentFen,
        };

        const response = await MakeMove(request);

        if (response.isGameEnded) {
          setWinner(response.winner);
        }

        // Update state with server response
        const newPieces = getPiecesFromFen(response.fen);
        setPieces(newPieces);
        setAllLegalMoves(response.legalMoves);
        console.log(response);
        setCurrentFen(response.fen);
        setMoveNotations(response.moveNotations);

        // Update last valid state
        lastValidBoardState.current = {
          pieces: newPieces,
          fen: response.fen,
        };

        setIsProcessingMove(false);
      }
    } catch (error: any) {
      // Revert to last valid state if there was an error
      setPieces(lastValidBoardState.current.pieces);
      setCurrentFen(lastValidBoardState.current.fen);
      setErrorAlertMessage(error.message);
      setIsProcessingMove(false);
      setStartSquare(null);
    }
  };

  const handleGameStart = async (color: 'white' | 'black') => {
    try {
      const response = await OnStartGame(color === 'white');
      console.log(response);
      const newPieces = getPiecesFromFen(response.fen);

      setCurrentFen(response.fen);
      setAllLegalMoves(response.legalMoves);
      setIsGameStarted(true);
      setPieces(newPieces);
      setMoveNotations(response.moveNotations);

      // Initialize last valid state
      lastValidBoardState.current = {
        pieces: newPieces,
        fen: response.fen,
      };
    } catch (error: any) {
      setErrorAlertMessage(error.message);
    }
  };

  const handleColorChange = (color: 'white' | 'black') => {
    setPlayerColor(color);
  };

  const isAlliedPiece = (piece?: ChessPiece) => {
    if (!piece) return false;
    if (isProcessingMove) return false; // Prevent piece selection while processing
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
      // Save current state before making changes
      lastValidBoardState.current = {
        pieces: pieces.map((piece) => ({ ...piece })),
        fen: currentFen,
      };

      setIsProcessingMove(true);
      try {
        // Show immediate feedback for promotion
        updatePiecePosition(startSquare, promotionData.squareIndex);

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

        const newPieces = getPiecesFromFen(data.fen);
        setPieces(newPieces);
        setCurrentFen(data.fen);
        setMoveNotations(data.moveNotations);
        setAllLegalMoves(data.legalMoves);
        setCurrentLegalMoves(undefined);

        // Update last valid state
        lastValidBoardState.current = {
          pieces: newPieces,
          fen: data.fen,
        };
      } catch (error: any) {
        // Revert to last valid state if there was an error
        setPieces(lastValidBoardState.current.pieces);
        setCurrentFen(lastValidBoardState.current.fen);
        setErrorAlertMessage(error.message);
      } finally {
        setIsProcessingMove(false);
        setPromotionData({ isOpen: false, squareIndex: null });
      }
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
        onClose={() => {
          if (!isProcessingMove) {
            setPromotionData({ isOpen: false, squareIndex: null });
          }
        }}
      />
      <Nav />
      <div className="container nav-padding">
        <div className="even-columns padding-block-400 max-height">
          <div>
            {winner && <Winner winnerName={winner} />}
            <div className="chessboard-wrapper">
              <div
                className={`chessboard ${isProcessingMove ? 'processing' : ''}`}
              >
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
                          } ${isProcessingMove ? 'processing' : ''}`}
                        />
                      )}
                      {currentLegalMoves?.includes(squareIndex) &&
                        !isProcessingMove && (
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
