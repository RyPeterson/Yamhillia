import { createContext, useContext } from "react";

interface SpinnerContextProps {
  ready(): void;
  loading(): void;
  isLoading: boolean;
}

const SpinnerContext = createContext<SpinnerContextProps>({
  ready: () => {},
  loading: () => {},
  isLoading: false,
});

export default SpinnerContext;

export function useSpinnerContext() {
  return useContext(SpinnerContext);
}
