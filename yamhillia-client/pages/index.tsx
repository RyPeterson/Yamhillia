import styled from "styled-components";
import Page from "../components/Page";
import getUser from "../api/user/getUser";
import authenticate from "../authenticate/authenticate";
import { context } from "@opentelemetry/api";

export default function Home() {
  return (
    <Page title="Home">
      <h1>hello world</h1>
    </Page>
  );
}

Home.getInitialProps = async (ctx) => {
  const user = await authenticate(ctx);
  return { user };
};

const MainContainer = styled.div``;
