import React, { useCallback, useState } from "react";
import UnauthenticatedRoot from "./roots/UnauthenticatedRoot";
import User from "./types/user/User";
import useAsyncEffect from "./hooks/utils/useAsyncEffect";
import getUser from "./api/user/getUser";
import AuthenticatedRoot from "./roots/AuthenticatedRoot";
import SpinnerOverlay from "./components/SpinnerOverlay";
import SpinnerContext from "./context/SpinnerContext";

function App() {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  useAsyncEffect(async (isMounted) => {
    const loadedUser = await getUser();
    if (isMounted()) {
      setUser(loadedUser);
    }
  }, []);

  const beginLoading = useCallback(() => {
    setLoading(true);
  }, []);

  const endLoading = useCallback(() => {
    setLoading(false);
  }, []);

  return (
    <>
      {loading && <SpinnerOverlay />}
      <SpinnerContext.Provider
        value={{ isLoading: loading, loading: beginLoading, ready: endLoading }}
      >
        {user ? (
          <AuthenticatedRoot user={user} />
        ) : (
          <UnauthenticatedRoot user={user} />
        )}
      </SpinnerContext.Provider>
    </>
  );
}

export default App;
