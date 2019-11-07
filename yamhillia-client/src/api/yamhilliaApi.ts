import Axios, { AxiosInstance, AxiosError } from "axios";
import Cookies from "js-cookie";
import * as UserEndpoints from "./UserEndpoints";
import * as UtilityEndpoints from "./UtilityEndpoints";
import { User } from "../models/User";

const loginCookieName = "fluffy";

const apiUrl = process.env.REACT_APP_API_URL || "http://localhost:80";

const axios: AxiosInstance = Axios.create({
  baseURL: `${apiUrl}/api/yamhillia`
});

axios.interceptors.request.use(conf => {
  conf.headers.common["Authorization"] = `Bearer ${Cookies.get(
    loginCookieName
  )}`;
  return conf;
});

const api = {
  ...UserEndpoints,
  ...UtilityEndpoints,
  login: async (axios: AxiosInstance, username: string, password: string) => {
    const userWithToken = await api._login(axios, username, password);
    if (userWithToken.token) {
      Cookies.set(loginCookieName, userWithToken.token);
      return userWithToken.user;
    }
    return null;
  },
  logout: () => {
    Cookies.remove(loginCookieName);
  },
  getUser: async (axios: AxiosInstance) => {
    const user = await api._getUser(axios);
    return user;
  },
  createUser: async (
    axios: AxiosInstance,
    data: UserEndpoints.CreateUserParams
  ): Promise<User | null> => {
    const result = await api._createUser(axios, data);
    if (result) {
      const { user, token } = result;
      if (token) {
        Cookies.remove(loginCookieName);
        Cookies.set(loginCookieName, token);
        return user;
      }
    }
    return null;
  }
};

export default bindAxiosToApi(api, axios);

function bindAxiosToApi(
  unboundApi: UnboundApi,
  axiosInstance: AxiosInstance
): BoundApi {
  const boundApi: Record<string, any> = {};
  for (const [key, value] of Object.entries(unboundApi)) {
    const fn = value as (axios: AxiosInstance, ...rest: any[]) => any;
    boundApi[key] = fn.bind(null, axiosInstance);
  }
  return boundApi as BoundApi;
}

type UnboundApi = typeof api;
type TailArgs<F> = F extends (first: any, ...rest: infer G) => any
  ? G
  : never[];
type BoundApi = {
  [K in keyof UnboundApi]: (
    ...args: TailArgs<UnboundApi[K]>
  ) => ReturnType<UnboundApi[K]>;
};
