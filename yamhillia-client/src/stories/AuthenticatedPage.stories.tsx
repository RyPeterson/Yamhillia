import React from "react";
import { BrowserRouter } from "react-router-dom";
import AuthenticatedPage from "../components/AuthenticatedPage";

export default {
  title: "AuthenticatedPage",
};

export const noStyles = () => (
  <BrowserRouter>
    <AuthenticatedPage user={{ email: "test@test.com" }} />
  </BrowserRouter>
);
