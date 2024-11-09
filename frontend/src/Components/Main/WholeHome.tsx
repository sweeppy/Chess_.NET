import { useNavigate } from "react-router-dom";
import Sidebar from "./Sidebar";
import Welcome from "./Welcome";

const MainPage = () => {
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
  const handelTelegramIconClick = () => {
    window.open("https://t.me/sweeppy");
  };
  return (
    <div className="even-columns no-scroll">
      <Welcome handleLoginClick={handleLoginClick} />
      <Sidebar
        handleLoginClick={handleLoginClick}
        handleGithubIconClick={handleGithubIconClick}
        handleTelegramIconClick={handelTelegramIconClick}
      />
    </div>
  );
};

export default MainPage;
