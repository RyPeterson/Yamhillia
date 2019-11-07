import React, { FC, useEffect, useState } from "react";
import Page from "./Page";
import withUser from "../utils/withUser";
import { Redirect } from "react-router";
import yamhilliaApi from "../api/yamhilliaApi";

const Logout: FC = props => {
  const [loggingOut, setLoggingOut] = useState(true);

  async function performLogout() {
    await yamhilliaApi.logout();
    setLoggingOut(false);
  }
  useEffect(() => {
    performLogout();
  }, []);

  if (!loggingOut) {
    return <Redirect to="/login" />;
  }

  return <Page loading />;
};

export default withUser(Logout, true);
