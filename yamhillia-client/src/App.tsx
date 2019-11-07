import React from "react";
import "./App.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Index from "./pages/Index";
import Register from "./pages/Register";
import Login from "./pages/Login";
import Logout from "./pages/Logout";
import Profile from "./pages/Profile";

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
      <Route exact path="/logout">
        <Logout />
      </Route>
      <Route exact path="/profile">
        <Profile />
      </Route>
    </Switch>
  </Router>
);

export default App;
