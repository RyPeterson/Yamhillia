import React, { FC, useEffect } from "react";
import styled from "styled-components/macro";
import Column from "./Column";
import { ThemeColor } from "../constants/colors";
import Nav, { NavigationItem } from "./Nav";
import User from "../types/user/User";

export interface PageComponentProps {
  title?: string;
  navigationItems?: ReadonlyArray<NavigationItem>;
  user?: User | null;
  hideNav?: boolean;
}

const Page: FC<PageComponentProps> = ({
  title,
  navigationItems = [],
  user,
  hideNav,
  children,
  ...rest
}) => {
  useEffect(() => {
    document.title = `Yamhillia ${title ? `- ${title}` : ""}`;
  }, [title]);

  return (
    <Root {...rest}>
      {!hideNav && <Nav user={user} navigationItems={navigationItems} />}
      <PageBody>{children}</PageBody>
    </Root>
  );
};

export default styled(Page)``;

const Root = styled(Column)`
  flex: 1 0 auto;
  max-width: 100vw;
  height: 100%;
  min-height: 100vh;
  overflow: hidden;
  background-color: ${ThemeColor.lightest};
`;

export const PageBody = styled(Column)`
  padding: 0 1.5rem 1rem;
  flex: 1 0 auto;
  overflow: hidden;
  overflow-y: auto;
`;
