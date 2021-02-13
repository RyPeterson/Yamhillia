import { NextPageContext } from "next";
import User from "../types/user/User";
import getUser from "../api/user/getUser";
import nextRedirect from "./nextRedirect";
import { login } from "../constants/routes/unauthenticatedRoutes";

export default async function authenticate(
  context: NextPageContext
): Promise<User> {
  const user = await getUser();
  if (user === null) {
    await nextRedirect({ to: login, context });
  }
  // We've redirected by now.
  return user as User;
}
