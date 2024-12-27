import { useNavigate } from "react-router-dom";

const WelcomePage = () => {
  // Use navigate
  const navigate = useNavigate();

  // For button that navigate to login page
  const handleLoginClick = () => {
    navigate("/Login");
  };
  // For Links:
  // Github
  const handleGithubIconClick = () => {
    window.open("https://github.com/sweeppy");
  };
  // Telegram
  const handleTelegramIconClick = () => {
    window.open("https://t.me/sweeppy");
  };
  return (
    <div className="container">
      <div className="even-columns no-scroll">
        <main className="main">
          <div data-column className="flex-center flow">
            <h1 className="inline-padding">Welccome, New Grandmaster!</h1>
            <img
              height={300}
              src="/design/game/assets/boards/persp_04.svg"
              alt="chess_board"
            />
            <button className="primary-button" onClick={handleLoginClick}>
              Start Chess Jorney
            </button>
          </div>
        </main>
        <div
          data-column
          className="sidebar inline-padding padding-block flex-center"
        >
          <h2 className="inline-padding padding-block-600">
            To start playing you need to log in
          </h2>
          <button
            className="primary-button max-width"
            onClick={handleLoginClick}
          >
            Login
          </button>
          <div data-column className="flex-center padding-block-600">
            <h2>Socials</h2>
            <div className="row-group">
              <div className="img-container">
                <img
                  width={40}
                  src="/design/web/icons/social/telegram_icon.svg"
                  alt="telegram_link"
                  onClick={handleTelegramIconClick}
                />
              </div>
              <div className="img-container">
                <img
                  width={40}
                  src="/design/web/icons/social/github_icon.svg"
                  alt="github_link"
                  onClick={handleGithubIconClick}
                />
              </div>
            </div>
            <h3 className="padding-block-400">Made by sweeppy</h3>
          </div>
        </div>
      </div>
    </div>
  );
};

export default WelcomePage;
