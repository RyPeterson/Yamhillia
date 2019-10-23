import { AxiosInstance } from "axios";

export async function login(
  axios: AxiosInstance,
  username: string,
  password: string
): Promise<string> {
  const result = await axios.post("/yamhillia/authentication/create-token", {
    username,
    password
  });
  const { data } = result;
  const { token } = data;
  return String(token);
}
