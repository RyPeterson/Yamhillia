import React, { FC, useEffect } from "react";
import styled from "styled-components/macro";
import UnauthenticatedPage from "../components/UnauthenticatedPage";
import PageProps from "../types/page/PageProps";
import { useSpinnerContext } from "../context/SpinnerContext";

const Login: FC<PageProps> = () => {
  const { ready } = useSpinnerContext();

  useEffect(() => {
    ready();
  }, [ready]);
  return (
    <UnauthenticatedPage hideNav title="Login">
      TODO
    </UnauthenticatedPage>
  );
};

export default styled(Login)``;
