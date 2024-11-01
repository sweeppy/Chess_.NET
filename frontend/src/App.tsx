import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import WholeHome from "./Components/Home/WholeHome";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<WholeHome />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
