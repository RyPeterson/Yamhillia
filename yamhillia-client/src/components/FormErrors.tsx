import React, { FC } from "react";
import styled from "styled-components/macro";
import FormError from "../types/components/FormError";
import { error } from "../constants/colors";

const FormErrors: FC<{ errors?: ReadonlyArray<FormError> }> = ({
  errors = [],
  ...rest
}) => {
  return (
    <Root {...rest}>
      {errors.map((err) => (
        <li key={err.id} role="alert" id={err.id}>
          {err.error}
        </li>
      ))}
    </Root>
  );
};

export default styled(FormErrors)``;

const Root = styled.ul`
  color: ${error};
`;
