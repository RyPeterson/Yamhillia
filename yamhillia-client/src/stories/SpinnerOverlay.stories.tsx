import React from "react";
import SpinnerOverlay from "../components/SpinnerOverlay";
import BlankButton from "../components/BlankButton";

export default {
  title: "SpinnerOverlay",
};

export const basic = () => <SpinnerOverlay />;

export const withChild = () => (
  <SpinnerOverlay>
    <BlankButton>Ha</BlankButton>
  </SpinnerOverlay>
);
