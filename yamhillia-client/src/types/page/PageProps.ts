import User from "../user/User";

export default interface PageProps {
  user?: User | null;
}

export interface AuthenticatedPageProps extends PageProps {
  user: User;
}
