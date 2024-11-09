import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import Login from "./Components/Account/Login";
import MainPage from "./Components/Main/WholeHome";
import Home from "./Components/Game/Home";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<MainPage />}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/home" element={<Home />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
