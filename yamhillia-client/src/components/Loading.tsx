import React, { FC, ReactNode } from "react";
import styled, { keyframes } from "styled-components/macro";
import Column from "./Column";
import { MoonLoader } from "react-spinners";

interface LoadingProps {
  children?: ReactNode;
}

const DefaultMessage: FC = props => (
  <>
    <div>Loading. </div>
    <div>Please Wait...</div>
  </>
);

const Loading: FC<LoadingProps> = ({ children, ...rest }) => (
  <Root {...rest}>
    <MoonLoader />
    {children || <DefaultMessage />}
  </Root>
);

export default styled(Loading)``;

const fadeIn = keyframes`
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
`;

const Root = styled(Column)`
  height: 100%;
  width: 100%;
  justify-content: center;
  align-items: center;
  opacity: 0;
  animation: ${fadeIn} 0.5s forwards;
`;
