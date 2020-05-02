import React, { FC } from "react";
import styled from "styled-components/macro";
import { Link } from "react-router-dom";
import Row from "./Row";
import Column from "./Column";
import theme, { background, foreground } from "../constants/theme";
import useUserContext from "../utils/UserContext";

const NavBar: FC = props => {
  const { user } = useUserContext();

  return (
    <Root>
      <YamhilliaNavItem>
        <Link to="/">Yamhillia</Link>
      </YamhilliaNavItem>
      <NavItems>
        <NavItem>
          <Link to="/">Home</Link>
        </NavItem>
        {user !== null ? (
          <NavItem>
            <Link to="/animals">Animals</Link>
          </NavItem>
        ) : null}
      </NavItems>
      {user !== null ? (
        <UserOperations>
          <NavItem>
            <Link to="/profile">Welcome {user.email}</Link>
          </NavItem>
          <NavItem>
            <Link to="/logout">Logout</Link>
          </NavItem>
        </UserOperations>
      ) : (
        <UserOperations>
          <NavItem>
            <Link to="/login">Login</Link>
          </NavItem>
          <NavItem>
            <Link to="/register">Register</Link>
          </NavItem>
        </UserOperations>
      )}
    </Root>
  );
};

const Root = styled(Row)`
  height: 50px;
  align-items: center;
  ${background(theme.darkest)}
  padding: 0 2rem;
`;

const NavItem = styled(Column)`
  align-items: center;
  a {
    text-decoration: none;
    ${foreground(theme.lightest)}
    &:hover {
      text-decoration: underline;
    }
  }
`;

const NavItems = styled(Row)`
    align-items: center;
    ${NavItem} + ${NavItem} {
        margin-left: 1rem;
    }
`;

const UserOperations = styled(Row)`
    align-items: center;
    margin-left: auto;
    ${NavItem} + ${NavItem} {
        margin-left: 1rem;
    }
`;

const YamhilliaNavItem = styled(NavItem)`
  font-weight: bold;
  font-size: 1.5em;
  margin-right: 2rem;
`;

export default NavBar;
