import Axios from "axios";
import { API_SERVER } from "../config";

const api = Axios.create({
  baseURL: `${API_SERVER}/api`,
});

export default api;
