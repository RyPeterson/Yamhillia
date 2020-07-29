import React, {
  ComponentPropsWithRef,
  FC,
  FormEvent,
  useCallback,
} from "react";
import styled from "styled-components/macro";
interface FormProps extends ComponentPropsWithRef<"form"> {
  onSubmit(): void | Promise<void>;
}

const Form: FC<FormProps> = ({ onSubmit, ...rest }) => {
  const handleSubmit = useCallback(
    async (event: FormEvent) => {
      event.preventDefault();
      onSubmit();
    },
    [onSubmit]
  );

  return <Root onSubmit={handleSubmit} {...rest} />;
};

export default styled(Form)``;

const Root = styled.form`
  display: flex;
  flex-direction: column;
`;
