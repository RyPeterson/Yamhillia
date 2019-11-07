import React, { FC } from "react";
import Page from "./Page";
import withUser from "../utils/withUser";

const Profile: FC = props => <Page>profile page</Page>;

export default withUser(Profile, true);
