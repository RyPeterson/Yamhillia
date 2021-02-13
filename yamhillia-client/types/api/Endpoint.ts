import {AxiosInstance} from "axios";

/**
 * Defines a endpoint to call that accepts some parameters and optionally an AxiosInstance (for SSR config)
 * and returns a result
 */
export type Endpoint<Params, Result> = (options: Params, axios?: AxiosInstance) => Promise<Result>;

/**
 * Defines and endpoint to call that doesn't require params and returns a Result.
 */
export type ParamlessEndpoint<Result> = (axios?: AxiosInstance) => Promise<Result>;


