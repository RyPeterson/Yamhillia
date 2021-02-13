import api from "../api";
import User from "../../types/user/User";

export default async function getUser(axios = api): Promise<User | null> {
  try {
    const response = await axios.get(`/auth/user`);
    return response.data.user;
  } catch (e) {
    if (e.response && e.response.status === 403) {
      // Not logged in
      return null;
    }
    // Some other error
    throw e;
  }
}
