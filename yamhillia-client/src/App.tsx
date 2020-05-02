import React from "react";
import "./App.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Index from "./pages/Index";
import Register from "./pages/Register";
import Login from "./pages/Login";
import Logout from "./pages/Logout";
import Profile from "./pages/Profile";
import Playground from "./pages/Playground";
import Animals from "./pages/Animals";

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
      <Route exact path="/animals">
        <Animals />
      </Route>
      {process.env.NODE_ENV === "development" && (
        <Route exact path="/playground">
          <Playground />
        </Route>
      )}
    </Switch>
  </Router>
);

export default App;
