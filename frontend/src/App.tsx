import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import WelcomePage from "./pages/welcome/WelcomePage";
import LoginPage from "./pages/authentication/LoginPage";
import HomePage from "./pages/chess/HomePage";
import PlayWithComputerPage from "./pages/chess/PlayWithComputerPage";
function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<WelcomePage />}></Route>
        <Route path="/login" element={<LoginPage />}></Route>
        <Route path="/home" element={<HomePage />}></Route>
        <Route path="/play/computer" element={<PlayWithComputerPage />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
