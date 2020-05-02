import React from "react";
import {
  faGenderless,
  faMars,
  faVenus,
  faNeuter
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styled from "styled-components/macro";
import Genders from "../models/Genders";
import theme from "./theme";

export const GenderIconBase = styled(FontAwesomeIcon)`
  color: ${theme.darker};
`;

export const OtherGenderIcon = styled(GenderIconBase).attrs({
  icon: faGenderless
})``;

export const MaleGenderIcon = styled(GenderIconBase).attrs({ icon: faMars })``;

export const FemaleGenderIcon = styled(GenderIconBase).attrs({
  icon: faVenus
})``;

export const NeuteredGenderIcon = styled(GenderIconBase).attrs({
  icon: faNeuter
})``;

type GenderToIconKey = {
  [key in Genders]: () => JSX.Element;
};

const GenderIcons: GenderToIconKey = {
  [Genders.MALE]: () => <MaleGenderIcon />,
  [Genders.FEMALE]: () => <FemaleGenderIcon />,
  [Genders.OTHER]: () => <OtherGenderIcon />,
  [Genders.NEUTERED]: () => <NeuteredGenderIcon />
};

export default GenderIcons;
