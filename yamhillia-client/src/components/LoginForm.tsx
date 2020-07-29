import React, { FC } from "react";
import styled from "styled-components/macro";
import Form from "./Form";
import FormInput from "./FormInput";
import { ButtonPrimary } from "./Button";
import FormError from "../types/components/FormError";
import FormErrors from "./FormErrors";

interface LoginFormProps {
  email: string;
  onEmailChanged(value: string): void;
  password: string;
  onPasswordChanged(value: string): void;
  onSubmit(): Promise<void>;
  errors?: ReadonlyArray<FormError>;
}

export const formErrors = {
  forEmail: "email-error",
  forPassword: "password-error",
};

const LoginForm: FC<LoginFormProps> = ({
  email,
  onEmailChanged,
  password,
  onPasswordChanged,
  errors,
  ...rest
}) => {
  return (
    <Root {...rest}>
      <FormInput
        id="email"
        label="Email"
        aria-describedby={formErrors.forEmail}
        value={email}
        onChange={onEmailChanged}
      />
      <FormInput
        id="password"
        type="password"
        label="Password"
        aria-describedby={formErrors.forPassword}
        value={password}
        onChange={onPasswordChanged}
      />
      <ButtonPrimary>Log In</ButtonPrimary>
      <FormErrors errors={errors} />
    </Root>
  );
};

export default styled(LoginForm)``;

const Root = styled(Form)`
  justify-content: space-between;
  flex: 1 0 auto;
  max-height: 300px;

  ${FormErrors} {
    min-height: 80px;
  }
`;
