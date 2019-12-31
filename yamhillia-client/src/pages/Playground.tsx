import React, { FC, useCallback, useState } from "react";
import { Redirect } from "react-router-dom";
import yamhilliaApi from "../api/yamhilliaApi";
import AnimalOverviewList from "../components/AnimalOverviewList";
import Animal from "../models/Animal";
import useEffectAsync from "../utils/useEffectAsync";
import withUser from "../utils/withUser";
import Page from "./Page";

const Playgound: FC = props => {
  const [animals, setAnimals] = useState<Animal[]>([]);
  const [loading, setLoading] = useState(true);

  useEffectAsync(async unmounted => {
    const animalList = await yamhilliaApi.getAnimals();
    if (!unmounted()) {
      setAnimals(animalList);
      setLoading(false);
    }
  }, []);

  const onAnimalClicked = useCallback(animal => {}, []);

  if (process.env.NODE_ENV !== "development") {
    return <Redirect to="/" />;
  }
  return (
    <Page loading={loading}>
      <AnimalOverviewList
        animals={animals}
        onAnimalDetailsClicked={onAnimalClicked}
      />
    </Page>
  );
};

export default withUser(Playgound);
