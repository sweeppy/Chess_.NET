import { useNavigate } from 'react-router-dom';

const WelcomePage = () => {
    // Use navigate
    const navigate = useNavigate();

    // For button that navigate to login page
    const handleLoginClick = () => {
        navigate('/Login');
    };
    // For Links:
    // Github
    const handleGithubIconClick = () => {
        window.open('https://github.com/sweeppy');
    };
    // Telegram
    const handleTelegramIconClick = () => {
        window.open('https://t.me/sweeppy');
    };
    return (
        <div className="container">
            <div className="even-columns no-scroll max-height">
                <div className="flex-column flex-center flow">
                    <h1 className="inline-padding">
                        Welcome, New Grandmaster!
                    </h1>
                    <img
                        height={300}
                        src="/src/assets/game/welcome_board.svg"
                        alt="chess_board"
                    />
                    <button
                        className="button button-accent"
                        onClick={handleLoginClick}
                    >
                        Start Chess Journey
                    </button>
                </div>
                <div className="flex-column flex-center">
                    <h2 className="inline-padding padding-block-600">
                        To start playing you need to log in
                    </h2>
                    <button
                        className="button button-accent max-width"
                        onClick={handleLoginClick}
                    >
                        Login
                    </button>
                    <div className="flex-center flex-column padding-block-600">
                        <h2>Socials</h2>
                        <div className="row-group">
                            <div className="img-container">
                                <img
                                    width={40}
                                    src="/src/assets/web/icons/social/telegram_icon.svg"
                                    alt="telegram_link"
                                    onClick={handleTelegramIconClick}
                                />
                            </div>
                            <div className="img-container">
                                <img
                                    width={40}
                                    src="/src/assets/web/icons/social/github_icon.svg"
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
