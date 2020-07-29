import React, { FC, useCallback, useEffect, useState } from "react";
import styled from "styled-components/macro";
import UnauthenticatedPage from "../components/UnauthenticatedPage";
import PageProps from "../types/page/PageProps";
import { useSpinnerContext } from "../context/SpinnerContext";
import Column from "../components/Column";
import useLoginForm from "../hooks/pages/login/useLoginForm";
import LoginForm, { formErrors } from "../components/LoginForm";
import login from "../api/user/login";
import FormError from "../types/components/FormError";

const Login: FC<PageProps> = () => {
  const { ready, loading } = useSpinnerContext();
  const [errors, setErrors] = useState<FormError[]>([]);
  const { email, password, ...restOfLoginProps } = useLoginForm();

  const onSubmit = useCallback(async () => {
    setErrors([]);
    const newErrors: FormError[] = [];
    if (!email.trim()) {
      newErrors.push({ id: formErrors.forEmail, error: "Email is invalid." });
    }
    if (password.length < 6) {
      newErrors.push({
        id: formErrors.forPassword,
        error: "Password is too short.",
      });
    }
    setErrors(newErrors);
    if (newErrors.length) {
      return;
    }

    try {
      loading();
      await login(email, password);
    } catch (e) {
      setErrors([
        {
          id: "auth-error",
          error:
            "Failed to Log In. Please verify the email and password is correct.",
        },
      ]);
      ready();
    }
  }, [email, password, loading, ready]);

  useEffect(() => {
    ready();
  }, [ready]);

  return (
    <UnauthenticatedPage hideNav title="Login">
      <Content>
        <h1>Yamhillia - Login</h1>
        <LoginForm
          email={email}
          password={password}
          onSubmit={onSubmit}
          errors={errors}
          {...restOfLoginProps}
        />
      </Content>
    </UnauthenticatedPage>
  );
};

export default styled(Login)`
  align-items: center;
  justify-content: center;
`;

const Content = styled(Column)`
  width: 100%;
  flex: 1 0 auto;
  align-items: center;
  ${LoginForm} {
    width: 100%;
    max-width: 300px;
  }
`;
