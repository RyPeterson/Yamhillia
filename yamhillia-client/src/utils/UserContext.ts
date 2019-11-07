import { createContext, useContext } from "react";
import { User } from "../models/User";

interface UserContextProps {
  user: User | null;
}
const UserContext = createContext<UserContextProps>({
  user: null
});

const { Provider: UserProvider, Consumer: UserConsumer } = UserContext;

export { UserProvider, UserConsumer };

export default function useUserContext() {
  return useContext(UserContext);
}
