import isServer from "./isServer";
import { NextPageContext } from "next";
import Router from "next/router";

interface RedirectOptions {
  to: string;
  context: NextPageContext;
}

export default async function nextRedirect({
  to,
  context,
}: RedirectOptions): Promise<boolean> {
  if (isServer) {
    return new Promise<boolean>((resolve) => {
      context.res.writeHead(307, { Location: to });
      context.res.end();
      resolve(true);
    });
  } else {
    return Router.replace(to);
  }
}
