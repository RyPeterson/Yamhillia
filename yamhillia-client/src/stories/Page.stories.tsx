import React from "react";
import StoryRouter from "storybook-react-router";
import Page from "../components/Page";

export default {
  title: "Page",
  decorators: [StoryRouter()],
};

export const noStyle = () => <Page>Page</Page>;
