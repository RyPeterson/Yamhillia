import React, {FC, useCallback, useState} from "react";
import styled from "styled-components";
import UnauthenticatedPage from "../components/UnauthenticatedPage";
import PageProps from "../types/page/PageProps";
import Column from "../components/Column";
import useLoginForm from "../hooks/pages/login/useLoginForm";
import LoginForm, { formErrors } from "../components/LoginForm";
import login from "../api/user/login";
import FormError from "../types/components/FormError";
import {useRouter} from "next/router";
import {home} from "../constants/routes/shared";

const Login: FC<PageProps> = () => {
  const [errors, setErrors] = useState<FormError[]>([]);
  const { email, password, ...restOfLoginProps } = useLoginForm();
  const router = useRouter();

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
      await login(email, password);
      await router.replace(home);
    } catch (e) {
      setErrors([
        {
          id: "auth-error",
          error:
            "Failed to Log In. Please verify the email and password is correct.",
        },
      ]);
    }
  }, [email, password, router]);

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
