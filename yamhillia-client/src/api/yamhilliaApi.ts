import Axios, { AxiosInstance, AxiosError } from "axios";

const axios: AxiosInstance = Axios.create({
  baseURL: `/api`
});

const api = {};

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
