import React, { FC } from "react";
import Page from "./Page";
import withUser from "../utils/withUser";

const Index: FC = props => <Page>index page</Page>;

export default withUser(Index);
