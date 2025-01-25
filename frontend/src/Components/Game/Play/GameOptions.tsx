import { useState } from "react";

const GameOptions = () => {
  const [chosenColor, setChosenColor] = useState("all-clr");
  const handleChooseClick = (choice: string) => {
    setChosenColor(choice);
  };

  const [chosenDifficulty, setChosenDifficulty] = useState<string | null>(null);

  const handleChooseDifficulty = (difficulty: string) => {
    setChosenDifficulty(difficulty);
  };
  return (
    <>
      <div className="flex-column game-options padding-block-400 inline-padding">
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
              src="/design/game/assets/chess_pieces/BW_King.svg"
              alt=""
            />
          </div>
          <div
            className={`choice white ${chosenColor === "white" ? "chosen" : ""}`}
            onClick={() => handleChooseClick("white")}
          >
            <img
              src="/design/game/assets/chess_pieces/W_King.svg"
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
              src="/design/game/assets/chess_pieces/B_King.svg"
              alt="black color"
            />
          </div>
        </div>
        <div className="difficulty-selector padding-block-600">
          <h2 className="fs-secondary-heading fw-semi-bold">
            Choose bot difficulty
          </h2>
          <div>
            <label>
              <input
                type="checkbox"
                name="difficulty"
                value="easy"
                checked={chosenDifficulty === "easy"}
                onChange={() => handleChooseDifficulty("easy")}
              />
              Easy
            </label>
          </div>
          <div>
            <label>
              <input
                type="checkbox"
                name="difficulty"
                value="medium"
                checked={chosenDifficulty === "medium"}
                onChange={() => handleChooseDifficulty("medium")}
              />
              Medium
            </label>
          </div>
          <div>
            <label>
              <input
                type="checkbox"
                name="difficulty"
                value="hard"
                checked={chosenDifficulty === "hard"}
                onChange={() => handleChooseDifficulty("hard")}
              />
              Hard
            </label>
          </div>
        </div>
      </div>
    </>
  );
};

export default GameOptions;
