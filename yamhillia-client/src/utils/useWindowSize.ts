import { useEffect, useState, useCallback } from "react";

//Adapted from https://gist.github.com/gragland/4e3d9b1c934a18dc76f585350f97e321
/**
 * Get the window's size (width and height).
 * This should not be used in place of proper css media queries.
 */
export default function useWindowSize() {
  const getSize = useCallback(
    () => ({ width: window.innerWidth, height: window.innerHeight }),
    []
  );

  const [windowSize, setWindowSize] = useState(getSize);

  const handleResize = useCallback(() => {
    setWindowSize(getSize);
  }, [getSize]);

  useEffect(() => {
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, [handleResize]);

  return windowSize;
}
