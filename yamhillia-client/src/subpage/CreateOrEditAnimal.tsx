import React, { FC } from "react";
import styled from "styled-components/macro";
import Animal from "../models/Animal";
import AnimalForm from "../components/AnimalForm";
import Column from "../components/Column";

interface AnimalOverviewProps {
  animal?: Animal;
  onChange(animal: Animal): void;
  onSubmit(): void;
}
const CreateOrEditAnimal: FC<AnimalOverviewProps> = ({
  animal,
  onChange,
  onSubmit,
  ...rest
}) => (
  <Root {...rest}>
    {animal && (
      <AnimalForm animal={animal} onChange={onChange} onSubmit={onSubmit} />
    )}
  </Root>
);

export default CreateOrEditAnimal;

const Root = styled(Column)`
  flex: 1 1 auto;
`;
