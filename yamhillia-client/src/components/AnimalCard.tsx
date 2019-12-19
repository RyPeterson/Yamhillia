import React, { FC } from "react";
import Animal from "../models/Animal";
import styled from "styled-components";
import Column from "./Column";
import AnimalImage from "./AnimalImage";

interface AnimalCardProps {
  animal: Animal;
}

const AnimalCard: FC<AnimalCardProps> = ({ animal, ...rest }) => (
  <AnimalCardRoot {...rest}>
    <AnimalImage
      image={animal.image}
      species={animal.species}
      alt={`Image ${animal.image ? "of" : "placeholder for"} ${animal.name}`}
    />
  </AnimalCardRoot>
);

export default styled(AnimalCard)``;

const AnimalCardRoot = styled(Column)`
  height: 500px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
  align-items: center;
  ${AnimalImage} {
    height: 30%;
    width: 80%;
  }
`;
