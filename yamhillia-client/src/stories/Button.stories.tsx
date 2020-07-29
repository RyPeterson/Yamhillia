import React from "react";
import Button, {
  ButtonPrimary,
  ButtonQuaternary,
  ButtonQuinary,
  ButtonSecondary,
  ButtonTertiary,
} from "../components/Button";
import Column from "../components/Column";
import { ThemeColor } from "../constants/colors";

export default {
  title: "Button",
};

export const baseButton = () => (
  <Button theme={ThemeColor.lighter}>Button</Button>
);

export const allOfThem = () => (
  <Column>
    <ButtonPrimary>Primary</ButtonPrimary>
    <ButtonSecondary>Secondary</ButtonSecondary>
    <ButtonTertiary>Tertiary</ButtonTertiary>
    <ButtonQuaternary>Quaternary</ButtonQuaternary>
    <ButtonQuinary>Quinary</ButtonQuinary>
  </Column>
);
