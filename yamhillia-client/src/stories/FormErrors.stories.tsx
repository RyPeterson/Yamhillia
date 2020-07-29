import React from "react";
import FormErrors from "../components/FormErrors";

export default {
  title: "FormErrors",
};

export const empty = () => <FormErrors errors={[]} />;

export const withErrors = () => (
  <FormErrors
    errors={[
      { id: "42", error: "An error occurred" },
      { id: "418", error: "Imma teapot" },
      {
        id: "bacon",
        error:
          "Bacon ipsum dolor amet cupim hamburger kielbasa capicola, chicken fatback prosciutto. Pork jerky fatback beef, filet mignon short loin pork chop meatball chislic shank ball tip sirloin beef ribs ground round. Cow bacon doner, meatball short loin pastrami spare ribs ball tip frankfurter ham hock jerky. Rump strip steak fatback shankle bresaola pancetta pastrami pig doner pork turducken cow flank biltong.",
      },
    ]}
  />
);
