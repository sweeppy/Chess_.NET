interface Props {
  handleLoginClick: () => void;
}
const Welcome = ({ handleLoginClick }: Props) => {
  return (
    <main className="main">
      <div data-column className="flex-center flow">
        <h1 className="inline-padding">Welccome, New Grandmaster!</h1>
        <img
          height={300}
          src="/public/web_design/images/welcome_board.svg"
          alt="chess_board"
        />
        <button className="primary-button" onClick={handleLoginClick}>
          Start Chess Jorney
        </button>
      </div>
    </main>
  );
};

export default Welcome;
