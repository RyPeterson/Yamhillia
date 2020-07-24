import api from "../api";

export default async function getUser() {
  try {
    const response = await api.get(`/auth/user`);
    return response.data.user;
  } catch (e) {
    if (e.response && e.response.status === 401) {
      // Not logged in
      return null;
    }
    // Some other error
    throw e;
  }
}
