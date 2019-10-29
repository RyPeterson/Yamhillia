import React, { FC } from "react";
import styled from "styled-components";
import { MoonLoader } from "react-spinners";
import Column from "../components/Column";
import NavBar from "../components/NavBar";
import theme, { background } from "../constants/theme";

interface PageProps {
  title?: string;
  loading?: boolean;
}

function setTitleWithMagic(title?: string) {
  if (title) {
    document.title = `Yamhillia - ${title}`;
  } else {
    document.title = "Yamhillia";
  }
}

const Page: FC<PageProps> = ({ children, title, loading, ...rest }) => {
  setTitleWithMagic(title);
  return (
    <PageRoot {...rest}>
      {loading ? (
        <Loading>
          <MoonLoader />
          <div>Loading. </div>
          <div>Please Wait...</div>
        </Loading>
      ) : (
        <>
          <NavBar />
          {children}
        </>
      )}
    </PageRoot>
  );
};

export default Page;

const PageRoot = styled(Column)`
  width: 100vw;
  height: 100vh;
  ${background(theme.lightest)}
`;

const Loading = styled(Column)`
  height: 100%
  width: 100%;
  justify-content: center;
  align-items: center;
`;
