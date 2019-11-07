import React, { useState, useEffect } from "react";
import { RouteComponentProps, withRouter, Redirect } from "react-router";
import { UserProvider } from "./UserContext";
import { User } from "../models/User";
import yamhilliaApi from "../api/yamhilliaApi";

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
      return null;
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
