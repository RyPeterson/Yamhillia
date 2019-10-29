import React, { FC, useState } from "react";
import styled from "styled-components";
import Page from "./Page";
import yamhilliaApi from "../api/yamhilliaApi";
import Column from "../components/Column";
import Button from "../components/Button";
import useInput from "../utils/useInput";
import { withRouter, RouterProps } from "react-router";

const Login: FC<RouterProps> = ({ history, ...rest }) => {
  const [email, onEmailChanged] = useInput("");
  const [password, onPasswordChanged] = useInput("");
  const [loading, setLoading] = useState(false);

  async function login() {
    try {
      setLoading(true);
      const user = await yamhilliaApi.login(email, password);
      if (user) {
        setLoading(false);
        history.push("/");
      }
    } finally {
      setLoading(false);
    }
  }

  async function handleSubmit(event: React.FormEvent<HTMLDivElement>) {
    event.preventDefault();
    await login();
  }

  return (
    <Page title="Login" loading={loading}>
      <LoginForm onSubmit={handleSubmit}>
        <label>Email</label>
        <EmailField value={email} onChange={onEmailChanged} />
        <label>Password </label>
        <PasswordField value={password} onChange={onPasswordChanged} />
        <Button onClick={login}>Login</Button>
      </LoginForm>
    </Page>
  );
};

export default withRouter(Login);

const EmailField = styled.input``;
const PasswordField = styled.input.attrs({ type: "password" })``;

const LoginForm = styled(Column).attrs({ as: "form" })`
  flex: 1;
  justify-content: center;
  align-items: center;
  input {
    width: 300px;
  }

  ${Button} {
    margin-top: 1rem;
  }
`;
