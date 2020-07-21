import React, { FC, useEffect } from "react";
import styled from "styled-components/macro";
import Column from "./Column";

interface PageProps {
  title?: string;
}

const Page: FC<PageProps> = ({ title, ...rest }) => {
  useEffect(() => {
    document.title = `Yamhillia ${title ? `- ${title}` : ""}`;
  }, [title]);

  return <Root {...rest}></Root>;
};

export default styled(Page)``;

const Root = styled(Column)`
  flex: 1 0 auto;
  height: 100%;
  min-height: 100vh;
  // Screw Safari. Doesn't look good on orientation change on mobile... so only desktop Safari is supported
  min-height: -webkit-fill-available;
  overflow: hidden;
  padding: 0 1.5rem 1rem;
`;
