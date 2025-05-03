import React from 'react';

interface PromotionProps {
  isOpen: boolean;
  playerColor: 'white' | 'black';
  onSelect: (pieceType: string) => void;
  onClose: () => void;
}

const Promotion: React.FC<PromotionProps> = ({
  isOpen,
  playerColor,
  onSelect,
  onClose,
}) => {
  if (!isOpen) return null;

  const promotionPieces =
    playerColor === 'white'
      ? ['W_Queen', 'W_Rook', 'W_Bishop', 'W_Knight']
      : ['B_Queen', 'B_Rook', 'B_Bishop', 'B_Knight'];

  return (
    <div className="promotion-modal">
      <div className="promotion-backdrop" onClick={onClose} />
      <div className="promotion-options">
        {promotionPieces.map((piece) => (
          <div
            key={piece}
            className="promotion-option"
            onClick={() => onSelect(piece)}
          >
            <img
              src={`/src/assets/game/chess_pieces/${piece}.svg`}
              alt={piece}
              className="promotion-piece"
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default Promotion;
