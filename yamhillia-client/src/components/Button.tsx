import React from "react";
import styled from "styled-components";
import Row from "./Row";
import theme, { background } from "../constants/theme";

interface Props {
  onClick?(): any;
}

const Button: React.FC<Props> = ({ onClick = () => {}, children, ...rest }) => (
  <ButtonRoot onClick={onClick} role="button" {...rest}>
    {children}
  </ButtonRoot>
);

export default styled(Button)``;

const ButtonRoot = styled("button")`
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 1em;
  cursor: pointer;
  height: 40px;
  width: 100px;
  border-radius: 5px;
  border: 1px solid ${theme.darker};
  ${background(theme.lighter)}
`;
