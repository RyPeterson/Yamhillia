import styled from "styled-components";
import Page from "../components/Page";
import authenticate from "../authenticate/authenticate";
import {NextPage, NextPageContext} from "next";
import {AuthenticatedPageProps} from "../types/page/PageProps";

const Home: NextPage<AuthenticatedPageProps> = ({user}) => {
  return (
    <Page title="Home" user={user}>
      <h1>hello world</h1>
    </Page>
  );
}

Home.getInitialProps = async (ctx: NextPageContext) => {
  const user = await authenticate(ctx);
  return { user };
};

export default Home;
