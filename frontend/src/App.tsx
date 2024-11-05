import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import WholeHome from "./Components/Home/WholeHome";
import Login from "./Components/Account/Login";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<WholeHome />}></Route>
        <Route path="/Login" element={<Login />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
