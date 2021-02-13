import React, { FC } from "react";
import styled from "styled-components";
import Page, { PageComponentProps } from "./Page";
import { NavigationItem } from "./Nav";

const navigationItems: ReadonlyArray<NavigationItem> = [
  { label: "Home", to: "/" },
  { label: "Animals", to: "/animals" },
];

const AuthenticatedPage: FC<PageComponentProps> = (props) => {
  return <Page navigationItems={navigationItems} {...props} />;
};

export default styled(AuthenticatedPage)``;
