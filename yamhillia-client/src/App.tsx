import React from "react";
import UnauthenticatedRoot from "./roots/UnauthenticatedRoot";
import { BrowserRouter } from "react-router-dom";

function App() {
  return (
    <BrowserRouter>
      <UnauthenticatedRoot />
    </BrowserRouter>
  );
}

export default App;
