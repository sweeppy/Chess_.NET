import React from 'react';

interface GameHistoryProps {
  moveNotations: string[] | null;
}

const GameHistory: React.FC<GameHistoryProps> = ({ moveNotations }) => {
  return (
    <>
      <div className="flex-column game-info padding-block-400 inline-padding">
        <h2 className="fs-secondary-heading fw-semi-bold">History</h2>
        <ul className="game-history">
          {moveNotations?.map((move, index) => (
            <li key={index}>
              {index % 2 === 0 ? `${Math.floor(index / 2) + 1}. ${move}` : move}
            </li>
          ))}
        </ul>
      </div>
    </>
  );
};

export default GameHistory;
