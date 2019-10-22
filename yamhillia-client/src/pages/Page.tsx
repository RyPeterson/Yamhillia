import React, { FC, useEffect } from "react";
import styled from "styled-components";
import Column from "../components/Column";
import NavBar from "../components/NavBar";
import theme, { background } from "../constants/theme";

interface PageProps {
  title?: string;
}

function setTitleWithMagic(title?: string) {
  if (title) {
    document.title = `Yamhillia - ${title}`;
  } else {
    document.title = "Yamhillia";
  }
}

const Page: FC<PageProps> = ({ children, title, ...rest }) => {
  useEffect(() => setTitleWithMagic(title), [title]);

  return (
    <PageRoot {...rest}>
      <NavBar />
      {children}
    </PageRoot>
  );
};

export default Page;

const PageRoot = styled(Column)`
  width: 100vw;
  height: 100vh;
  ${background(theme.lightest)}
`;
