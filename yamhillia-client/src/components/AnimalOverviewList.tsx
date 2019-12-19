import React, { FC, useMemo } from "react";
import styled, { css, FlattenSimpleInterpolation } from "styled-components";
import chunk from "lodash/chunk";
import Column from "./Column";
import Animal from "../models/Animal";
import Row from "./Row";
import AnimalCard from "./AnimalCard";

interface AnimalOverviewListProps {
  animals: Animal[];
  cardsPerRow?: number;
}

const AnimalOverviewList: FC<AnimalOverviewListProps> = props => {
  const { animals, cardsPerRow = 5, ...rest } = props;
  const animalsPerRow = Math.max(cardsPerRow, 1);

  const animalRows = useMemo(
    () =>
      chunk(animals, animalsPerRow).map((animalRow, idx) => ({
        animals: animalRow,
        key: idx
      })),
    [animalsPerRow]
  );

  return (
    <ListRoot {...rest}>
      {animalRows.map(({ animals, key }) => (
        <AnimalRow key={key}>
          {animals.map(animal => (
            <AnimalCard key={animal.id} animal={animal} />
          ))}
        </AnimalRow>
      ))}
    </ListRoot>
  );
};

export default styled(AnimalOverviewList)``;

const AnimalRow = styled(Row)`
  justify-content: space-between;
  align-items: center;
  width: 90%;
  ${AnimalCard} {
    min-width: 300px;
  }
`;

const ListRoot = styled(Column)`
    width: 100%;
    height: 100%;
    justify-content: space-evenly;
    align-items: center;
    ${AnimalRow} + ${AnimalRow} {
        margin-top: 2rem;
    }
`;
