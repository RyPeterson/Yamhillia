import React, { FC } from "react";
import Page from "./Page";
import withUser from "../utils/withUser";
import useUserContext from "../utils/UserContext";
import { Redirect } from "react-router";

const Register: FC = props => {
  const { user } = useUserContext();

  if (user !== null) {
    return <Redirect to={"/"} />;
  }

  return <Page title="Register">register page</Page>;
};

export default withUser(Register);
