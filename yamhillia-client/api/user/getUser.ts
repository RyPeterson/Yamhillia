import api from "../api";
import User from "../../types/user/User";

export default async function getUser(): Promise<User | null> {
  try {
    const response = await api.get(`/auth/user`);
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
