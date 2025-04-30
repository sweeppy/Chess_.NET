import React from 'react';

interface WinnerProps {
  winnerName: string;
}

const Winner: React.FC<WinnerProps> = ({ winnerName }) => {
  return (
    <div className="winner">
      {winnerName == 'DRAW' ? 'Draw' : `Winner: ${winnerName}`}{' '}
    </div>
  );
};

export default Winner;
