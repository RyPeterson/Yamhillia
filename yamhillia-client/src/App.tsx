import React, { useState } from "react";
import UnauthenticatedRoot from "./roots/UnauthenticatedRoot";
import User from "./types/user/User";
import useAsyncEffect from "./hooks/utils/useAsyncEffect";
import getUser from "./api/user/getUser";
import AuthenticatedRoot from "./roots/AuthenticatedRoot";

function App() {
  const [user, setUser] = useState<User | null>(null);

  useAsyncEffect(async (isMounted) => {
    const loadedUser = await getUser();
    if (isMounted()) {
      setUser(loadedUser);
    }
  }, []);

  if (!!user) {
    return <AuthenticatedRoot user={user} />;
  }

  return <UnauthenticatedRoot />;
}

export default App;
