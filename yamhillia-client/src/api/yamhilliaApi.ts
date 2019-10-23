import Axios, { AxiosInstance, AxiosError } from "axios";
import * as UserEndpoints from "./UserEndpoints";
import * as UtilityEndpoints from "./UtilityEndpoints";

const apiUrl = process.env.REACT_APP_API_URL || "http://localhost:80";
console.log(process.env);

const axios: AxiosInstance = Axios.create({
  baseURL: `${apiUrl}/api/yamhillia`
});

const api = {
  ...UserEndpoints,
  ...UtilityEndpoints
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
