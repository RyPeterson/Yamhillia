import User from "../user/User";

export default interface RootProps {
  user: User | null;
}

export interface UnauthenticatedRootProps extends RootProps {
  user: null;
}

export interface AuthenticatedRootProps extends RootProps {
  user: User;
}
