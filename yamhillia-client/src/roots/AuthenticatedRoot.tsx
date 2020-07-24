import { BrowserRouter, Route, Switch } from "react-router-dom";
import { home, logout } from "../constants/routes/shared";
import Home from "../pages/Home";
import React, { FC } from "react";
import Logout from "../pages/Logout";
import { AuthenticatedRootProps } from "../types/page/RootProps";

const AuthenticatedRoot: FC<AuthenticatedRootProps> = ({ user }) => {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path={home}>
          <Home user={user} />
        </Route>
        <Route path={logout}>
          <Logout />
        </Route>
        {/* fallback */}
        <Route>
          <Home user={user} />
        </Route>
      </Switch>
    </BrowserRouter>
  );
};

export default AuthenticatedRoot;
