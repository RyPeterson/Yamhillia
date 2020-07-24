import { BrowserRouter, Route, Switch } from "react-router-dom";
import { home, logout } from "../constants/routes/shared";
import Home from "../pages/Home";
import React, { FC } from "react";
import Logout from "../pages/Logout";
import User from "../types/user/User";

interface AuthenticatedRootProps {
  user: User;
}

const AuthenticatedRoot: FC<AuthenticatedRootProps> = ({ user }) => {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path={home}>
          <Home />
        </Route>
        <Route path={logout}>
          <Logout />
        </Route>
        {/* fallback */}
        <Route>
          <Home />
        </Route>
      </Switch>
    </BrowserRouter>
  );
};

export default AuthenticatedRoot;
