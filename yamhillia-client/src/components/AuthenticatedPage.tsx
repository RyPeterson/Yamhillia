import React, { FC } from "react";
import styled from "styled-components/macro";
import Page from "./Page";
import { AuthenticatedPageProps } from "../types/page/PageProps";
import { NavigationItem } from "./Nav";

const navigationItems: ReadonlyArray<NavigationItem> = [
  { label: "Home", to: "/" },
  { label: "Animals", to: "/animals" },
];

const AuthenticatedPage: FC<AuthenticatedPageProps> = (props) => {
  return <Page navigationItems={navigationItems} {...props} />;
};

export default styled(AuthenticatedPage)``;
