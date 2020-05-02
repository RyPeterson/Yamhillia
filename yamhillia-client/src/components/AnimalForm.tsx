import React, { FC, useCallback, useState } from "react";
import styled from "styled-components/macro";
import Animal from "../models/Animal";
import Column from "./Column";
import Row from "./Row";

interface AnimalFormProps {
  animal: Animal;
  onChange(animal: Animal): void;
  onSubmit(): void;
}

const AnimalForm: FC<AnimalFormProps> = ({
  animal,
  onChange,
  onSubmit,
  ...rest
}) => {
  const handleSubmit = useCallback(() => {
    onSubmit();
  }, [onSubmit]);

  return (
    <Root onSubmit={handleSubmit} {...rest}>
      <h1>Create Animal</h1>
      <FieldGroup>
        <label id="a-f-name">Name:</label>
        <input
          aria-labelledby="a-f-name"
          type="text"
          value={animal.name}
          onChange={e => onChange({ ...animal, name: e.target.value })}
        />
      </FieldGroup>
    </Root>
  );
};

export default styled(AnimalForm)``;

const Root = styled(Column).attrs({ as: "form" })`
  flex: 1 1 auto;
  align-items: center;
`;

const FieldGroup = styled(Row)`
  align-items: center;
  justify-content: space-between;
`;
