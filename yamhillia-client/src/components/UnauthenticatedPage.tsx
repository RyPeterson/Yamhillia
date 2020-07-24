import React, { FC } from "react";
import styled from "styled-components/macro";
import { NavigationItem } from "./Nav";
import Page, { PageComponentProps } from "./Page";

const navigationItems: ReadonlyArray<NavigationItem> = [
  { label: "Home", to: "/" },
];

const UnauthenticatedPage: FC<PageComponentProps> = (props) => {
  return <Page navigationItems={navigationItems} {...props} />;
};

export default styled(UnauthenticatedPage)``;
