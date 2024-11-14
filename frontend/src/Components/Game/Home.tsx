const Home = () => {
  return (
    <div className="even-columns no-scroll">
      <nav className="primary-nav flow inline-padding">
        <h1 className="fs-primary-heading">PixelChess</h1>
        <div className="upper-border">
          <h3 data-scaleble>Profile</h3>
        </div>
        <div className="upper-border">
          <h3 data-scaleble>Settings</h3>
        </div>
        <div className="upper-border">
          <h3 data-scaleble>Friends</h3>
        </div>
        <div className="upper-border">
          <h3 data-scaleble>History</h3>
        </div>
      </nav>
      <div className="home even-columns padding-block-400">
        <div className="board">
          <img
            width={"100%"}
            src="/public/game/home_board.svg"
            alt="chess_board"
          />
        </div>
        <div className="play-menu padding-block-400 inline-padding">
          <h1 className="fs-primary-heading">Play chess</h1>

          <div className="item fs-small inline-padding fw-bold">
            <div className="img-container">
              <img
                src="/public/web_design/icons/play/lightning_icon.svg"
                alt="lighting_icon"
              />
            </div>
            <div className="text-center">Play online</div>
          </div>

          <div className="item fs-small inline-padding fw-bold">
            <div className="img-container">
              <img
                src="/public/web_design/icons/play/user_icon.svg"
                alt="friend_icon"
              />
            </div>
            <div className="text-center">Play with friend</div>
          </div>

          <div className="item fs-small inline-padding fw-bold">
            <div className="img-container">
              <img
                src="/public/web_design/icons/play/computer_icon.svg"
                alt="computer_icon"
              />
            </div>
            <div className="text-center">Play with computer</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Home;
