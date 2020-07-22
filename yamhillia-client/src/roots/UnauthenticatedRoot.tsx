import React, { FC } from "react";
import { Switch, Route } from "react-router-dom";
import { home } from "../constants/routes/shared";
import Home from "../pages/Home";

const UnauthenticatedRoot: FC = () => {
  return (
    <Switch>
      <Route exact path={home}>
        <Home />
      </Route>
    </Switch>
  );
};

export default UnauthenticatedRoot;
