import { AxiosInstance } from "axios";
import { UserWithToken } from "../models/UserWithToken";

export async function _login(
  axios: AxiosInstance,
  username: string,
  password: string
): Promise<UserWithToken> {
  const result = await axios.post("/authentication/login", {
    username,
    password
  });
  const { data } = result;
  const { user } = data;

  return {
    user: user.user,
    token: user.token
  };
}

export async function _getUser(axios: AxiosInstance) {
  const response = await axios.get("/authentication/user").catch(e => {
    /* 401, probably */ return null;
  });
  if (response) {
    return response.data.user;
  }
  return null;
}

export interface CreateUserParams {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export async function _createUser(
  axios: AxiosInstance,
  params: CreateUserParams
): Promise<UserWithToken | null> {
  const result = await axios.post("/authentication/register", {
    ...params
  });
  const { data } = result;
  const { token, user } = data;
  return { token, user };
}
