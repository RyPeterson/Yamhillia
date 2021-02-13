import Axios, {AxiosRequestConfig} from "axios";
import { API_SERVER } from "../config";
import {NextPageContext} from "next";
import isServer from "../authenticate/isServer";

const axiosConfig: AxiosRequestConfig = {
  baseURL: `${API_SERVER}/api`,
  withCredentials: true,
}

const api = Axios.create({
  ...axiosConfig
});

export default api;

/**
 * An axios instance to be used in SSR.
 * @param context
 */
export const serverApi = (context: NextPageContext)  =>{
  return Axios.create({...axiosConfig, headers: context.req?.headers });
};

export const getApi = (context?: NextPageContext) => {
  if(isServer && context) {
    return serverApi(context);
  }
  return api;
}
