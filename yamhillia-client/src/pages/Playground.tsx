import React, { FC, useState, useEffect } from "react";
import Page from "./Page";
import withUser from "../utils/withUser";
import { Redirect } from "react-router-dom";
import Animal from "../models/Animal";
import useEffectAsync from "../utils/useEffectAsync";
import yamhilliaApi from "../api/yamhilliaApi";
import AnimalOverviewList from "../components/AnimalOverviewList";
import useWindowSize from "../utils/useWindowSize";

const Playgound: FC = props => {
  const [animals, setAnimals] = useState<Animal[]>([]);
  const [cardsPerRow, setCardsPerRow] = useState(5);
  const [loading, setLoading] = useState(true);

  useEffectAsync(async unmounted => {
    const animalList = await yamhilliaApi.getAnimals();
    if (!unmounted()) {
      setAnimals(animalList);
      setLoading(false);
    }
  }, []);

  const windowSize = useWindowSize();
  useEffect(() => {}, [windowSize]);

  if (process.env.NODE_ENV !== "development") {
    return <Redirect to="/" />;
  }
  return (
    <Page loading={loading}>
      <AnimalOverviewList animals={animals} cardsPerRow={cardsPerRow} />
    </Page>
  );
};

export default withUser(Playgound);
