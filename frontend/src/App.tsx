import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import Login from "./Components/Account/Login";
import Home from "./Components/Game/Home";
import WelcomePage from "./Components/Main/WelcomePage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<WelcomePage />}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/home" element={<Home />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
