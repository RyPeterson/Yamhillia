import React, { FC } from "react";
import styled from "styled-components";
import UnauthenticatedPage from "../components/UnauthenticatedPage";
import PageProps from "../types/page/PageProps";

const Register: FC<PageProps> = () => {
  return (
    <UnauthenticatedPage hideNav title="Register">
      TODO
    </UnauthenticatedPage>
  );
};

export default styled(Register)``;
