import React, { FC, useState, useEffect } from "react";
import {
  Redirect,
  RouteComponentProps,
  RouteChildrenProps
} from "react-router";
import isEmpty from "validator/lib/isEmpty";
import isEmail from "validator/lib/isEmail";
import styled from "styled-components";
import Button from "../components/Button";
import Column from "../components/Column";
import getInstallationMode, {
  InstallationModes
} from "../constants/installationMode";
import useInput from "../utils/useInput";
import useUserContext from "../utils/UserContext";
import withUser from "../utils/withUser";
import Page from "./Page";
import Row from "../components/Row";
import Loading from "../components/Loading";
import yamhilliaApi from "../api/yamhilliaApi";
import validatePassword from "../utils/validatePassword";

const Register: FC<RouteComponentProps> = props => {
  const { user } = useUserContext();
  const mode = getInstallationMode();
  const [loading, setLoading] = useState(false);

  if (user !== null) {
    return <Redirect to={"/"} />;
  }

  return (
    <Page title="Register" loading={loading}>
      {mode === InstallationModes.INTERNET ? (
        <RegisterForm {...props} onLoading={l => setLoading(l)} />
      ) : (
        <ContactAdminPage {...props} />
      )}
    </Page>
  );
};

interface RegisterFormProps extends RouteChildrenProps {
  onLoading(loading: boolean): void;
}
const RegisterForm: FC<RegisterFormProps> = ({
  onLoading,
  history,
  ...rest
}) => {
  const [email, onEmailChanged] = useInput("");
  const [firstName, onFirstNameChanged] = useInput("");
  const [lastName, onLastNameChanged] = useInput("");
  const [password, onPasswordChanged] = useInput("");
  const [passwordConfirm, onPasswordConfirmChanged] = useInput("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function handleSubmit() {
    if (loading) return;
    let nextError = "";
    if (isEmpty(email) || !isEmail(email)) {
      nextError = "Invalid Email";
    } else if (isEmpty(password)) {
      nextError = "Password is required";
    } else if (!validatePassword(password)) {
      nextError =
        "Password is invalid. Must be at least 6 characters, have at least 3 unique characters, and requires one digit and one non-alphanumeric character";
    } else if (password !== passwordConfirm) {
      nextError = "Password and password confirm do not match";
    }
    setError(nextError);
    if (nextError.length > 0) return;
    setLoading(true);
    try {
      const user = await yamhilliaApi.createUser({
        email,
        password,
        firstName,
        lastName
      });
      if (user) {
        history.push("/");
      }
    } catch (e) {
      setError("Request to server failed.");
    } finally {
      setLoading(false);
    }
  }

  async function handleFormSubmit(event: React.FormEvent<HTMLDivElement>) {
    event.preventDefault();
    await handleSubmit();
  }

  useEffect(() => {
    onLoading(loading);
  }, [loading, onLoading]);
  return (
    <PageBody>
      <RegisterationForm onSubmit={handleFormSubmit}>
        <label>Email</label>
        <TextField value={email} onChange={onEmailChanged} required />
        <label>First Name</label>
        <TextField value={firstName} onChange={onFirstNameChanged} />
        <label>Last Name</label>
        <TextField value={lastName} onChange={onLastNameChanged} />
        <label>Password</label>
        <PasswordField value={password} onChange={onPasswordChanged} required />
        <label>Confirm Password</label>
        <PasswordField
          value={passwordConfirm}
          onChange={onPasswordConfirmChanged}
          required
        />
        <FormError>{error}</FormError>
        <Button onClick={handleSubmit}>Register</Button>
      </RegisterationForm>
    </PageBody>
  );
};

const ContactAdminPage: FC<RouteComponentProps> = ({ history, ...rest }) => (
  <PageBody>
    <IntranetMessage>
      <div>
        Please contact the administrators of this organization or farm to get
        access.
      </div>
      <Button onClick={() => history.push("/")}>OKAY</Button>
    </IntranetMessage>
  </PageBody>
);

export default withUser(Register);

const PageBody = styled(Column)`
  flex: 1;
`;

const IntranetMessage = styled(Column)`
  justify-content: center;
  align-items: center;
  flex: 1;
  ${Button} {
    margin-top: 1rem;
  }
`;

const RegisterationForm = styled(Column).attrs({ as: "form" })`
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

const TextField = styled.input``;
const PasswordField = styled.input.attrs({ type: "password" })``;

const FormError = styled(Row)`
  color: red;
  height: 20px;
`;
