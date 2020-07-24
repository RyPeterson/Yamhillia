import User from "../user/User";

export default interface PageProps {
  user?: User | null;
  hideNav?: boolean;
  title?: string;
}

export interface AuthenticatedPageProps extends PageProps {
  user: User;
}
