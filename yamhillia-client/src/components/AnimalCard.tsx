import React, { FC } from "react";
import styled from "styled-components";
import GenderIcons, { GenderIconBase } from "../constants/genderIcons";
import theme, { foreground } from "../constants/theme";
import Animal from "../models/Animal";
import AnimalImage from "./AnimalImage";
import Button from "./Button";
import Column from "./Column";
import Row from "./Row";

interface AnimalCardProps {
  animal: Animal;
  onAnimalDetailsClicked(): void;
}

const AnimalCard: FC<AnimalCardProps> = ({
  animal,
  onAnimalDetailsClicked,
  ...rest
}) => (
  <AnimalCardRoot {...rest}>
    <AnimalImage
      image={animal.image}
      species={animal.species}
      alt={`Image ${animal.image ? "of" : "placeholder for"} ${animal.name}`}
    />
    <Name>
      <div>{animal.name}</div>
    </Name>
    <BasicInfo>
      <Info>
        <div>Gender:&nbsp;</div>
        <div>{animal.gender}</div>
        <GenderIconContainer>
          {GenderIcons[animal.gender]()}
        </GenderIconContainer>
      </Info>
      <Info>
        <div>Species:&nbsp;</div>
        <div>{animal.species}</div>
      </Info>
    </BasicInfo>
    <ControlButtons>
      <Button onClick={onAnimalDetailsClicked}>Details</Button>
    </ControlButtons>
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

const Name = styled(Row)`
  border-top: 1px solid ${theme.darker};
  border-bottom: 1px solid ${theme.darker};
  ${foreground(theme.darkest)}
  justify-content: center;
  align-items: center;
  margin-top: 0.5rem;
  width: 100%;
`;

const Info = styled(Row)`
  padding: 0.5rem;
`;

const GenderIconContainer = styled(Row)`
  width: 100%;
  align-items: center;
  ${GenderIconBase} {
    margin-left: auto;
    margin-right: 1rem;
  }
`;

const BasicInfo = styled(Column)`
  flex: 1;
  width: 100%;
`;

const ControlButtons = styled(Row)`
  justify-content: center;
  align-items: center;
  margin-bottom: 0.2rem;
`;
