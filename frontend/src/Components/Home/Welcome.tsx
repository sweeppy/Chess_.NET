const Welcome = () => {
  return (
    <main className="main">
      <div data-column className="flex-center flow">
        <h1 className="inline-padding">Welccome, New Grandmaster!</h1>
        <div className="img-container">
          <img
            height={300}
            src="/public/web_design/images/welcome_board.svg"
            alt="chess_board"
          />
        </div>
        <button className="primary-button">Start Chess Jorney</button>
      </div>
    </main>
  );
};

export default Welcome;
