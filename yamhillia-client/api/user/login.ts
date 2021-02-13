import api from "../api";
import {Endpoint} from "../../types/api/Endpoint";

interface Params {
  username: string;
  password: string
}

const login: Endpoint<Params, void> = ({username, password}: Params, axios = api ) => {
  return api.post("/auth/login", { Username: username, Password: password });
}

export default login;