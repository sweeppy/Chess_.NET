import React, { useState } from "react";

interface GameOptionsProps {
  OnColorChange: (color: "white" | "black") => void;
  OnGameStart: () => void;
}

const GameOptions: React.FC<GameOptionsProps> = ({
  OnColorChange,
  OnGameStart,
}) => {
  // color of the player
  const [chosenColor, setChosenColor] = useState<"white" | "black" | "all-clr">(
    "all-clr"
  );
  // set player's color
  const handleChooseClick = (choice: "white" | "black" | "all-clr") => {
    setChosenColor(choice);
  };
  // bot difficulty
  const [chosenDifficulty, setChosenDifficulty] = useState("easy");
  // set bot difficulty
  const handleChooseDifficulty = (difficulty: string) => {
    setChosenDifficulty(difficulty);
  };

  const handelStartGame = () => {
    const color = getFinalColor(chosenColor);
    OnColorChange(color);
    OnGameStart();
  };

  const getFinalColor = (
    color: "white" | "black" | "all-clr"
  ): "white" | "black" => {
    if (color === "white" || color === "black") return color;
    const number = Math.floor(Math.random() * 2) + 1;

    if (number === 1) return "white";
    else return "black";
  };

  return (
    <>
      <div className="flex-column game-info padding-block-400 inline-padding">
        <h2 className="fs-secondary-heading fw-semi-bold">Choose color</h2>
        <div className="padding-block-600 choice-wrapper">
          <div
            className={`choice all-clr ${
              chosenColor === "all-clr" ? "chosen" : ""
            }`}
            onClick={() => handleChooseClick("all-clr")}
          >
            <img
              width={30}
              src="/src/assets/game/chess_pieces/BW_King.svg"
              alt=""
            />
          </div>
          <div
            className={`choice white ${chosenColor === "white" ? "chosen" : ""}`}
            onClick={() => handleChooseClick("white")}
          >
            <img
              src="/src/assets/game/chess_pieces/W_King.svg"
              width={30}
              alt="white color"
            />
          </div>
          <div
            className={`choice black ${chosenColor === "black" ? "chosen" : ""}`}
            onClick={() => handleChooseClick("black")}
          >
            <img
              width={30}
              src="/src/assets/game/chess_pieces/B_King.svg"
              alt="black color"
            />
          </div>
        </div>

        <div className="difficulty-wrapper padding-block-600">
          <h2 className="fs-secondary-heading fw-semi-bold">
            Choose bot difficulty
          </h2>
          <div className="difficulty-selector">
            <input
              type="checkbox"
              name="difficulty"
              value="easy"
              checked={chosenDifficulty === "easy"}
              onChange={() => handleChooseDifficulty("easy")}
            />
            <label>Easy</label>
          </div>
          <div className="difficulty-selector">
            <input
              type="checkbox"
              name="difficulty"
              value="medium"
              checked={chosenDifficulty === "medium"}
              onChange={() => handleChooseDifficulty("medium")}
            />
            <label>Medium</label>
          </div>
          <div className="difficulty-selector">
            <input
              type="checkbox"
              name="difficulty"
              value="hard"
              checked={chosenDifficulty === "hard"}
              onChange={() => handleChooseDifficulty("hard")}
            />
            <label>Hard</label>
          </div>
        </div>

        <button
          className="button button-accent big-button"
          onClick={() => handelStartGame()}
        >
          Start game
        </button>
      </div>
    </>
  );
};

export default GameOptions;
