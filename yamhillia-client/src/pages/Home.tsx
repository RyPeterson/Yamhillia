import React, { FC, useEffect } from "react";
import styled from "styled-components/macro";
import PageProps from "../types/page/PageProps";
import UnauthenticatedPage from "../components/UnauthenticatedPage";
import { useSpinnerContext } from "../context/SpinnerContext";

const Home: FC<PageProps> = ({ ...rest }) => {
  const { ready } = useSpinnerContext();

  useEffect(() => {
    ready();
  }, [ready]);

  return (
    <UnauthenticatedPage {...rest}>
      <PageContents />
    </UnauthenticatedPage>
  );
};

export default styled(Home)``;

const PageContents: FC = () => <div>TODO</div>;
