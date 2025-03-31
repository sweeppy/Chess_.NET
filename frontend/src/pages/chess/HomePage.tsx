import { useNavigate } from 'react-router-dom';
import Nav from '../../components/chess/Nav';

const HomePage = () => {
    const navigate = useNavigate();

    // Redirect to play with computer page
    const handlePlayComputerClick = () => {
        navigate('/play/computer');
    };

    return (
        <>
            <Nav />
            <div className="container no-scroll">
                <div className="even-columns max-height">
                    <div>
                        <img
                            width={500}
                            src="/src/assets/game/home_board.svg"
                            alt="board"
                        />
                    </div>
                    <div className="flex-column flex-center play-menu">
                        <h1 className="fs-800 fw-bold padding-block-200">
                            Play now!
                        </h1>
                        <ul className="flex-column column-items">
                            <button className="button wide-button">
                                Play online
                            </button>
                            <button
                                onClick={handlePlayComputerClick}
                                className="button wide-button"
                            >
                                Play with computer
                            </button>
                            <button className="button wide-button">
                                Play with friends
                            </button>
                        </ul>
                    </div>
                </div>
            </div>
        </>
    );
};

export default HomePage;
