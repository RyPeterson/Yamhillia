import React, { FC } from "react";
import Head from "next/head";

interface Props {
  pageTitle?: string;
}

const DocumentHead: FC<Props> = ({ pageTitle }) => (
  <Head>
    <title>{`Yamhillia${pageTitle ? ` - ${pageTitle}` : ""}`}</title>
    <link rel="icon" href="/favicon.ico" />
  </Head>
);

export default DocumentHead;
