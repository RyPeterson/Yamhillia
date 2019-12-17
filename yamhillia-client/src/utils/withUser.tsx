import React, { useEffect, useState } from "react";
import { Redirect, RouteComponentProps, withRouter } from "react-router";
import yamhilliaApi from "../api/yamhilliaApi";
import { User } from "../models/User";
import LoadingPage from "../pages/LoadingPage";
import { UserProvider } from "./UserContext";

export default function withUser(
  Component: any,
  protect?: boolean,
  bumpTo?: string
) {
  const WithUserComponent: React.FC<RouteComponentProps<any>> = ({
    history,
    ...rest
  }) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    async function load() {
      setUser(await yamhilliaApi.getUser());
      setLoading(false);
    }
    useEffect(() => {
      load();
    }, []);

    if (loading) {
      return <LoadingPage />;
    }

    if (!loading && protect && user === null) {
      return <Redirect to={bumpTo || "/login"} />;
    }

    return (
      <UserProvider value={{ user }}>
        <Component history={history} user={user} {...rest} />
      </UserProvider>
    );
  };

  return withRouter(WithUserComponent);
}
