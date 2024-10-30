import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Welcome from "./Components/Home/Welcome";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<Welcome />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
