import React, { FC } from "react";
import styled from "styled-components/macro";
import PageProps from "../types/page/PageProps";
import UnauthenticatedPage from "../components/UnauthenticatedPage";

const Home: FC<PageProps> = (props) => {
  return (
    <UnauthenticatedPage {...props}>
      <PageContents />
    </UnauthenticatedPage>
  );
};

export default styled(Home)``;

const PageContents: FC = () => <div>TODO</div>;
