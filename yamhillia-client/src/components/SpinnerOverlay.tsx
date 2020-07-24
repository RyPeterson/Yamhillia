import React, { FC } from "react";
import styled from "styled-components/macro";
import SyncLoader from "react-spinners/SyncLoader";
import Overlay from "./Overlay";
import { ThemeColor } from "../constants/colors";
import Column from "./Column";

const SpinnerOverlay: FC = ({ children, ...rest }) => {
  return (
    <Root {...rest}>
      <SpinnerContainer>
        <SyncLoader color={ThemeColor.darkest} />
        {children}
      </SpinnerContainer>
    </Root>
  );
};

export default styled(SpinnerOverlay)``;

const Root = styled(Overlay)`
  justify-content: center;
  align-items: center;
`;

const SpinnerContainer = styled(Column)`
  align-items: center;
`;
