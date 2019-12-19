import { useEffect } from "react";

type Unmounted = () => boolean;
type FunctionBody = (isCancelled: Unmounted) => Promise<any>;

export default function useEffectAsync(
  body: FunctionBody,
  dependencies: ReadonlyArray<any> | undefined,
  onError?: (error: any) => void
): void {
  const errorHandler = onError || handleError;
  useEffect(() => {
    let unmounted = false;

    body(() => unmounted).catch(errorHandler);

    return () => {
      unmounted = true;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, dependencies);
}

function handleError(error: any) {
  console.error(error);
}
