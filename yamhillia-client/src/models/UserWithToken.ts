import { User } from "./User";

export interface UserWithToken {
  user: User;
  token: string;
}
