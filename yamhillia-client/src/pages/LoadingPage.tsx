import React, { FC } from "react";
import styled from "styled-components";
import Loading from "../components/Loading";
import { fadeIn } from "../constants/animations";
import hideable from "../utils/hideable";
import Page from "./Page";

const LoadingPage: FC = props => (
  <Page title="Loading" hideNav>
    <LoadingSpinner />
  </Page>
);

export default LoadingPage;

const LoadingSpinner = styled(Loading)`
    ${hideable()}
    animation: ${fadeIn} 0.5s forwards;
`;
