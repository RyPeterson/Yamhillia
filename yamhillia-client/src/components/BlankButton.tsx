import React, { ComponentPropsWithoutRef } from "react";
import styled from "styled-components/macro";

export type BlankButtonProps = ComponentPropsWithoutRef<"button">;

const BlankButton = styled.button<BlankButtonProps>`
  border: none;
  background-color: transparent;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: row;
  position: relative;
  font-size: 1em;
  font-family: "Roboto", sans-serif;

  &:hover {
    cursor: pointer;
  }
`;

export default BlankButton;
