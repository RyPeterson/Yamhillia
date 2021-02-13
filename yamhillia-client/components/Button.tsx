import styled, { css } from "styled-components";
import BlankButton, { BlankButtonProps } from "./BlankButton";
import { getContrastingColor, ThemeColor } from "../constants/colors";

interface Props extends BlankButtonProps {
  theme: ThemeColor;
}

const Button = styled(BlankButton)<Props>`
  height: 40px;
  padding: 0 1rem;
  justify-content: center;
  align-items: center;
  border-radius: 10px;
  font-weight: bold;

  ${(props) =>
    props.theme &&
    css`
      background-color: ${props.theme};
      color: ${getContrastingColor(props.theme)};
    `};
`;

export default Button;

export const ButtonPrimary = styled(Button).attrs({
  theme: ThemeColor.darkest,
})``;

export const ButtonSecondary = styled(Button).attrs({
  theme: ThemeColor.dark,
})``;

export const ButtonTertiary = styled(Button).attrs({
  theme: ThemeColor.base,
})``;

/*
    Probably should reconsider life choices if we're this deep
 */

export const ButtonQuaternary = styled(Button).attrs({
  theme: ThemeColor.lighter,
})``;

export const ButtonQuinary = styled(Button).attrs({
  theme: ThemeColor.lightest,
})``;
