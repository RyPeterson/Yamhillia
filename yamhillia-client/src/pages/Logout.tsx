import React, { FC, useEffect } from "react";
import styled from "styled-components/macro";
import Page from "../components/Page";
import PageProps from "../types/page/PageProps";
import { useSpinnerContext } from "../context/SpinnerContext";

const Logout: FC<PageProps> = () => {
  const { ready } = useSpinnerContext();

  useEffect(() => {
    ready();
  }, [ready]);
  return <Page>TODO</Page>;
};

export default styled(Logout)``;
