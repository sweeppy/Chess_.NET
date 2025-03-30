import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'

import WelcomePage from './pages/welcome/WelcomePage'
import LoginPage from './pages/authentication/LoginPage'
import HomePage from './pages/chess/HomePage'
import PlayWithComputerPage from './pages/chess/PlayWithComputerPage'
import CreateAccountPage from './pages/authentication/CreateAccountPage'
import ProtectedRoute from './pages/authentication/ProtectedRoute'
function App() {
    return (
        <Router>
            <Routes>
                <Route path="" element={<WelcomePage />}></Route>
                <Route path="/login" element={<LoginPage />}></Route>
                <Route path="/home" element={<HomePage />}></Route>
                <Route
                    path="/play/computer"
                    element={<PlayWithComputerPage />}
                ></Route>
                <Route element={<ProtectedRoute />}>
                    <Route
                        path="/createAccount"
                        element={<CreateAccountPage />}
                    ></Route>
                </Route>
            </Routes>
        </Router>
    )
}

export default App
