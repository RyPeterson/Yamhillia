import React from "react";
import "./App.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Index from "./pages/Index";
import Register from "./pages/Register";
import Login from "./pages/Login";

const App: React.FC = () => (
  <Router>
    <Switch>
      <Route exact path="/">
        <Index />
      </Route>
      <Route exact path="/register">
        <Register />
      </Route>
      <Route exact path="/login">
        <Login />
      </Route>
    </Switch>
  </Router>
);

export default App;
