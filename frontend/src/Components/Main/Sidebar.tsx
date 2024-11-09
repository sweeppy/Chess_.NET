interface Props {
  handleLoginClick: () => void;
  handleTelegramIconClick: () => void;
  handleGithubIconClick: () => void;
}
const Sidebar = ({
  handleLoginClick,
  handleGithubIconClick,
  handleTelegramIconClick,
}: Props) => {
  return (
    <div
      data-column
      className="sidebar inline-padding padding-block flex-center"
    >
      <h2 className="inline-padding padding-block-600">
        To start playing you need to log in
      </h2>
      <button className="primary-button max-width" onClick={handleLoginClick}>
        Login
      </button>
      <div data-column className="flex-center padding-block-600">
        <h2>Socials</h2>
        <div className="row-group">
          <div className="img-container">
            <img
              width={40}
              src="/web_design/icons/social/telegram_icon.svg"
              alt="telegram_link"
              onClick={handleTelegramIconClick}
            />
          </div>
          <div className="img-container">
            <img
              width={40}
              src="/public/web_design/icons/social/github_icon.svg"
              alt="github_link"
              onClick={handleGithubIconClick}
            />
          </div>
        </div>
        <h3 className="padding-block-400">Made by sweeppy</h3>
      </div>
    </div>
  );
};

export default Sidebar;
