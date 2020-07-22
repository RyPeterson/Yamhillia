import React, { FC } from "react";
import styled from "styled-components/macro";
import { NavigationItem } from "./Nav";
import Page from "./Page";
import PageProps from "../types/page/PageProps";

const navigationItems: ReadonlyArray<NavigationItem> = [
  { label: "Home", to: "/" },
];

const UnauthenticatedPage: FC<PageProps> = (props) => {
  return <Page navigationItems={navigationItems} {...props} />;
};

export default styled(UnauthenticatedPage)``;
