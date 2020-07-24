import React, { FC } from "react";
import { Switch, Route, BrowserRouter } from "react-router-dom";
import { home } from "../constants/routes/shared";
import Home from "../pages/Home";
import { login, register } from "../constants/routes/unauthenticatedRoutes";
import Login from "../pages/Login";
import Register from "../pages/Register";
import { UnauthenticatedRootProps } from "../types/page/RootProps";

const UnauthenticatedRoot: FC<UnauthenticatedRootProps> = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path={home}>
          <Home />
        </Route>
        <Route path={login}>
          <Login />
        </Route>
        <Route path={register}>
          <Register />
        </Route>
        {/* fallback */}
        <Route>
          <Home />
        </Route>
      </Switch>
    </BrowserRouter>
  );
};

export default UnauthenticatedRoot;
