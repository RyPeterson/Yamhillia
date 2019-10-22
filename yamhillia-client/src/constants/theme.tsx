import { css } from "styled-components";

const theme = {
  lightest: "#eafff8",
  lightererer: "#bafee6",
  lighterer: "#74fdcd",
  lighter: "#2ffcb4",
  medianLight: "#03e193",
  medianDark: "#028c5c",
  darker: "#027d52",
  darkerer: "#026d47",
  darkererer: "#015d3d",
  darkest: "#014e33"
};

const foreground = (color: string) =>
  css`
    color: ${color};
  `;

const background = (color: string) =>
  css`
    background-color: ${color};
  `;

export default theme;

export { foreground, background };
