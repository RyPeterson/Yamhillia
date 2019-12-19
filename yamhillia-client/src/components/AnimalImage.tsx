import React, { FC } from "react";
import AnimalSpecies from "../models/AnimalSpecies";
import defaultAnimalImages from "../constants/defaultAnimalImages";
import styled from "styled-components";

interface AnimalImageProps {
  image: string | null;
  species: AnimalSpecies;
  alt: string;
}

const AnimalImage: FC<AnimalImageProps> = ({
  image,
  species,
  alt,
  ...rest
}) => (
  <AnimalImageRoot
    src={image || defaultAnimalImages[species]}
    alt={alt}
    {...rest}
  />
);

export default styled(AnimalImage)``;

const AnimalImageRoot = styled.img``;
