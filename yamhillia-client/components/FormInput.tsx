import React, { ChangeEvent, FC, useCallback } from "react";
import styled from "styled-components";
import Column from "./Column";

interface FormInputProps {
  id: string;
  label: string;
  type?: "text" | "password";
  value?: string;
  onChange?(value: string): void;
}
const FormInput: FC<FormInputProps> = ({
  id,
  label,
  type = "text",
  onChange,
  value,
  ...rest
}) => {
  const handleChange = useCallback(
    (event: ChangeEvent<HTMLInputElement>) => {
      if (onChange) {
        onChange(event.target.value);
      }
    },
    [onChange]
  );

  return (
    <Column {...rest}>
      <Label htmlFor={id}>{label}</Label>
      <Input id={id} type={type} onChange={handleChange} value={value} />
    </Column>
  );
};

export default styled(FormInput)``;

const Input = styled.input`
  font-family: "Roboto", sans-serif;
`;

const Label = styled.label`
  font-family: "Roboto", sans-serif;
`;
