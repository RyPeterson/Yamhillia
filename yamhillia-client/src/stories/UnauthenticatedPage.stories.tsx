import React from "react";
import UnauthenticatedPage from "../components/UnauthenticatedPage";
import { BrowserRouter } from "react-router-dom";

export default {
  title: "UnauthenticatedPage",
};

export const noStyles = () => (
  <BrowserRouter>
    <UnauthenticatedPage />
  </BrowserRouter>
);
