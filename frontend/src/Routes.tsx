import { Route, Routes as Router, BrowserRouter } from "react-router-dom";

import { Welcome } from "./pages/Welcome";

const Routes = () => {
  return (
    <BrowserRouter>
      <Router>
        <Route path="/" element={<Welcome />} />
      </Router>
    </BrowserRouter>
  );
};

export default Routes;
