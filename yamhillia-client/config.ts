import { env } from "process";

export const API_SERVER = env.REACT_APP_API_SERVER || "http://localhost:5000";
