import React, { FC } from "react";
import styled from "styled-components/macro";
import AnimalOverviewList from "../components/AnimalOverviewList";
import Button from "../components/Button";
import Row from "../components/Row";
import Animal from "../models/Animal";

interface AnimalOverviewProps {
  animals: Animal[];
  onCreateAnimalClicked(): void;
  onAnimalClicked(animal: Animal): void;
}
const AnimalOverview: FC<AnimalOverviewProps> = ({
  animals,
  onAnimalClicked,
  onCreateAnimalClicked
}) => (
  <>
    <PageOperations>
      <CreateAnimal onClick={onCreateAnimalClicked}>Create Animal</CreateAnimal>
    </PageOperations>
    <AnimalOverviewList
      animals={animals}
      onAnimalDetailsClicked={onAnimalClicked}
    />
  </>
);

export default AnimalOverview;

const PageOperations = styled(Row)`
  justify-content: flex-end;
  padding: 0.25rem 0.5rem;
`;

const CreateAnimal = styled(Button)`
  width: 10rem;
`;
