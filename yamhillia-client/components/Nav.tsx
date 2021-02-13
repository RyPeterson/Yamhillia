import React, { FC } from "react";
import styled from "styled-components";
import Link from "next/link";
import Row from "./Row";
import BlankButton from "./BlankButton";
import { ThemeColor, ThemeContrastColor } from "../constants/colors";
import User from "../types/user/User";
import { login, register } from "../constants/routes/unauthenticatedRoutes";
import { logout } from "../constants/routes/shared";
import { profile } from "../constants/routes/authenticatedRoutes";

export interface NavigationItem {
  label: string;
  to: string;
}

interface NavProps {
  user?: User | null;
  navigationItems?: ReadonlyArray<NavigationItem>;
}

const Nav: FC<NavProps> = ({ user, navigationItems = [], ...rest }) => {
  return (
    <Root {...rest}>
      <NavItems>
        {navigationItems.map((item) => (
          <Link key={item.to} href={item.to}>
            <NavItem>{item.label}</NavItem>
          </Link>
        ))}
      </NavItems>
      {user ? <LoggedInNavigations user={user} /> : <NotLoggedInNavigations />}
    </Root>
  );
};

export default styled(Nav)``;

const Root = styled(Row)`
  background-color: ${ThemeColor.darkest};
  height: 60px;
  align-items: center;
  padding: 0 2rem;

  a {
    text-decoration: none;
    color: ${ThemeContrastColor.contrastDarkest};
    font-size: 1rem;
    &:hover {
      text-decoration: underline;
    }
  }
`;

const NavItem = styled(BlankButton)`
  color: ${ThemeContrastColor.contrastDarkest};
  height: 80%;
  align-items: center;
  justify-content: center;
`;

const NavItems = styled(Row)`
  flex: 1 0 60%;
  align-items: center;
  height: 100%;

  a + a {
    margin-left: 1rem;
  }
`;

const Welcome = styled(Row)`
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 500px;
  color: ${ThemeContrastColor.contrastDarkest};
`;

const AccountGroup = styled(Row)`
  height: 100%;
  align-self: flex-end;
  margin-left: auto;
  align-items: center;
  flex: 1 0 auto;
  justify-content: flex-end;
  button + button {
    margin-left: 1rem;
  }
`;

const LoggedInNavigations: FC<{ user: User }> = ({ user }) => (
  <AccountGroup>
    <Link href={profile}>
      <NavItem>Welcome {user.username}</NavItem>
    </Link>
    <Link href={logout}>
      <NavItem>Log Out</NavItem>
    </Link>
  </AccountGroup>
);

const NotLoggedInNavigations = () => (
  <AccountGroup>
    <Welcome>Welcome...</Welcome>
    <Link href={login}>
      <NavItem>Log In</NavItem>
    </Link>
    <Link href={register}>Register</Link>
  </AccountGroup>
);
