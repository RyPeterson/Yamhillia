import { AxiosInstance } from "axios";

export async function pingServer(axios: AxiosInstance): Promise<void> {
  const result = await axios.get("/");
  console.log(result);
}
