import React from "react";
import Nav from "../components/Nav";
import { BrowserRouter } from "react-router-dom";
import User from "../types/user/User";

export default {
  title: "Nav",
};

export const basicNavigation = () => (
  <BrowserRouter>
    <Nav />
  </BrowserRouter>
);

const navigationItems = [
  { label: "Home", to: "/" },
  { label: "Animals", to: "/" },
  { label: "A very long item", to: "/" },
  { label: "An even bigger item because haha", to: "/" },
  { label: "I", to: "/" },
  { label: "Another", to: "/" },
  { label: "And Another", to: "/" },
];

export const basicWithMenu = () => (
  <BrowserRouter>
    <Nav navigationItems={navigationItems} />
  </BrowserRouter>
);

const genUser = (nameLength: number): User => ({
  email: Array(nameLength).join("F"),
});

export const withUsers = () => (
  <BrowserRouter>
    <Nav user={genUser(1)} />
    <Nav user={genUser(2)} />
    <Nav user={genUser(3)} />
    <Nav user={genUser(4)} />
    <Nav user={genUser(5)} />
    <Nav user={genUser(10)} />
    <Nav user={genUser(15)} />
    <Nav user={genUser(20)} />
    <Nav user={genUser(25)} />
    <Nav user={genUser(30)} />
    <Nav user={genUser(60)} />
    <Nav user={genUser(90)} />
    <Nav user={genUser(120)} />
    <Nav user={genUser(128)} />
    <Nav user={genUser(250)} />
    <Nav user={genUser(255)} />
  </BrowserRouter>
);

export const withUsersAndMenu = () => (
  <BrowserRouter>
    <Nav navigationItems={navigationItems} user={genUser(1)} />
    <Nav navigationItems={navigationItems} user={genUser(2)} />
    <Nav navigationItems={navigationItems} user={genUser(3)} />
    <Nav navigationItems={navigationItems} user={genUser(4)} />
    <Nav navigationItems={navigationItems} user={genUser(5)} />
    <Nav navigationItems={navigationItems} user={genUser(10)} />
    <Nav navigationItems={navigationItems} user={genUser(15)} />
    <Nav navigationItems={navigationItems} user={genUser(20)} />
    <Nav navigationItems={navigationItems} user={genUser(25)} />
    <Nav navigationItems={navigationItems} user={genUser(30)} />
    <Nav navigationItems={navigationItems} user={genUser(60)} />
    <Nav navigationItems={navigationItems} user={genUser(90)} />
    <Nav navigationItems={navigationItems} user={genUser(120)} />
    <Nav navigationItems={navigationItems} user={genUser(128)} />
    <Nav navigationItems={navigationItems} user={genUser(250)} />
    <Nav navigationItems={navigationItems} user={genUser(255)} />
  </BrowserRouter>
);
