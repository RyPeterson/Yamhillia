import { DependencyList, useEffect } from "react";

type IsMounted = () => boolean;
type EffectFunction = (isMounted: IsMounted) => Promise<void>;

export default function useAsyncEffect(
  effect: EffectFunction,
  dependencies: DependencyList
) {
  useEffect(() => {
    let mounted = true;
    effect(() => mounted).catch(console.error);
    return () => {
      mounted = false;
    };
  }, [dependencies]);
}
