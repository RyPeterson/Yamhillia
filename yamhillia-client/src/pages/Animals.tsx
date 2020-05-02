import React, { FC, useCallback, useState } from "react";
import styled from "styled-components/macro";
import yamhilliaApi from "../api/yamhilliaApi";
import Animal from "../models/Animal";
import useEffectAsync from "../utils/useEffectAsync";
import withUser from "../utils/withUser";
import Page from "./Page";
import AnimalOverview from "../subpage/AnimalOverview";
import CreateOrEditAnimal from "../subpage/CreateOrEditAnimal";
import AnimalSpecies from "../models/AnimalSpecies";
import Genders from "../models/Genders";

enum PageState {
  overview,
  create,
  details
}

const DefaultCreateAnimalModel: Animal = {
  id: "",
  name: "",
  species: AnimalSpecies.CHICKEN,
  gender: Genders.MALE,
  image: "",
  customIdentifier: "",
  dateOfBirth: "",
  dateOfDeath: ""
};

const Animals: FC = props => {
  const [loading, setLoading] = useState(true);
  const [animals, setAnimals] = useState<Animal[]>([]);
  const [pageState, setPageState] = useState<PageState>(PageState.create);
  const [animalToEdit, setAnimalToEdit] = useState<Animal | null>({
    ...DefaultCreateAnimalModel
  });

  useEffectAsync(async isCancelled => {
    const animalList = await yamhilliaApi.getAnimals();
    if (!isCancelled()) {
      setAnimals(animalList);
      setLoading(false);
    }
  }, []);

  const onAnimalClicked = useCallback(animal => {
    setPageState(PageState.details);
  }, []);

  const onCreateAnimalClicked = useCallback(() => {
    setAnimalToEdit({ ...DefaultCreateAnimalModel });
    setPageState(PageState.create);
  }, []);

  const onCreateAnimalChanged = useCallback(animal => {
    setAnimalToEdit({ ...animal });
  }, []);

  const createAnimal = useCallback(() => {}, []);

  const getContents = useCallback(() => {
    switch (pageState) {
      case PageState.overview:
        return (
          <AnimalOverview
            animals={animals}
            onAnimalClicked={onAnimalClicked}
            onCreateAnimalClicked={onCreateAnimalClicked}
          />
        );
      case PageState.create:
        return (
          <CreateOrEditAnimal
            animal={animalToEdit || undefined}
            onChange={onCreateAnimalChanged}
            onSubmit={createAnimal}
          />
        );
      default:
        return null;
    }
  }, [
    animalToEdit,
    animals,
    createAnimal,
    onAnimalClicked,
    onCreateAnimalChanged,
    onCreateAnimalClicked,
    pageState
  ]);

  return <Page loading={loading}>{getContents()}</Page>;
};

export default withUser(Animals, true);
