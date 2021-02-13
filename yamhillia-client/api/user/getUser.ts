import api from "../api";
import User from "../../types/user/User";
import { ParamlessEndpoint } from "../../types/api/Endpoint";

const getUser: ParamlessEndpoint<User | null > = async (axios = api) => {
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

export default getUser;
