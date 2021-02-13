import api from "../api";

export default async function login(username: string, password: string) {
  return api.post("/auth/login", { Username: username, Password: password });
}
