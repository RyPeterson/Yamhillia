import React from "react";
import LoginForm from "../components/LoginForm";

export default {
  title: "LoginForm",
};

export const form = () => (
  <LoginForm
    email="user@test.com"
    password="123456"
    onSubmit={async () => {}}
    onEmailChanged={() => {}}
    onPasswordChanged={() => {}}
  />
);
