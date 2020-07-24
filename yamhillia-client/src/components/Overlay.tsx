import styled from "styled-components/macro";
import { ThemeColor } from "../constants/colors";

const Overlay = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 9999;
  display: flex;
  flex-direction: column;
  background-color: ${ThemeColor.base};
`;

export default Overlay;
