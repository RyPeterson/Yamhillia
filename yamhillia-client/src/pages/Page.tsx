import React, { FC } from "react";
import styled from "styled-components";
import Column from "../components/Column";
import NavBar from "../components/NavBar";
import theme, { background } from "../constants/theme";
import Loading from "../components/Loading";

interface PageProps {
  title?: string;
  loading?: boolean;
  hideNav?: boolean;
}

function setTitleWithMagic(title?: string) {
  if (title) {
    document.title = `Yamhillia - ${title}`;
  } else {
    document.title = "Yamhillia";
  }
}

const Page: FC<PageProps> = ({
  children,
  title,
  loading,
  hideNav,
  ...rest
}) => {
  setTitleWithMagic(title);
  return (
    <PageRoot {...rest}>
      {loading ? (
        <Loading />
      ) : (
        <>
          {!hideNav && <NavBar />}
          {children}
        </>
      )}
    </PageRoot>
  );
};

export default Page;

const PageRoot = styled(Column)`
  flex: 1 1 auto;
  min-height: 100vh;
  ${background(theme.lightest)}

  ${Loading} {
    flex: 1;
  }
`;
