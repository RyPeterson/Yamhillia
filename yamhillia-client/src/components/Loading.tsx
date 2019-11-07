import React, { FC } from "react";
import styled from "styled-components";
import Column from "./Column";
import { MoonLoader } from "react-spinners";

const Loading: FC = props => (
  <Root {...props}>
    <MoonLoader />
    <div>Loading. </div>
    <div>Please Wait...</div>
  </Root>
);

export default styled(Loading)``;

const Root = styled(Column)`
  height: 100%;
  width: 100%;
  justify-content: center;
  align-items: center;
`;
