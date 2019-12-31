import React, { FC } from "react";
import styled from "styled-components";
import Animal from "../models/Animal";
import AnimalCard from "./AnimalCard";
import Row from "./Row";

interface AnimalOverviewListProps {
  animals: Animal[];
  onAnimalDetailsClicked(animal: Animal): void;
}

const AnimalOverviewList: FC<AnimalOverviewListProps> = props => {
  const { animals, onAnimalDetailsClicked, ...rest } = props;
  return (
    <ListRoot {...rest}>
      {animals.map(animal => (
        <AnimalCard
          key={animal.id}
          animal={animal}
          onAnimalDetailsClicked={() => onAnimalDetailsClicked(animal)}
        />
      ))}
    </ListRoot>
  );
};

export default styled(AnimalOverviewList)``;

const ListRoot = styled(Row)`
  width: 100%;
  height: 100%;
  justify-content: space-evenly;
  flex-wrap: wrap;
  align-items: center;
  ${AnimalCard} {
    flex: 0 1 400px;
    margin: 0.5rem auto;
  }
`;
