import React, { useState } from "react";
import UnauthenticatedRoot from "./roots/UnauthenticatedRoot";
import User from "./types/user/User";
import useAsyncEffect from "./hooks/utils/useAsyncEffect";

function App() {
  const [user, setUser] = useState<User | null>(null);

  useAsyncEffect(async (isCancelled) => {}, []);

  return <UnauthenticatedRoot />;
}

export default App;
