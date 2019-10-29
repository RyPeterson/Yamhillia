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
  const response = await axios.get("/authentication/user");
  return response.data.user;
}
