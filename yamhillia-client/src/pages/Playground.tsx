import React, { FC } from "react";
import Page from "./Page";
import withUser from "../utils/withUser";
import { Redirect } from "react-router-dom";

const Playgound: FC = props => {
  if (process.env.NODE_ENV !== "development") {
    return <Redirect to="/" />;
  }
  return <Page>Playgound page</Page>;
};

export default withUser(Playgound);
