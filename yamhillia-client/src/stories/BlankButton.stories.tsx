import React, { FC, useCallback, useState } from "react";
import BlankButton from "../components/BlankButton";

export default {
  title: "BlankButton",
};

export const noStyles = () => <BlankButton>Blank Button</BlankButton>;

export const WithOnClick: FC = () => {
  const [fun, setFun] = useState(false);
  return (
    <BlankButton onClick={() => setFun((prev) => !prev)}>
      {fun ? "Its Fun!" : "Its Not Fun :("}
    </BlankButton>
  );
};
