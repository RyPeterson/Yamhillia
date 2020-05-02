import { AxiosInstance } from "axios";
import shuffle from "lodash/shuffle";
import Animal from "../models/Animal";
import AnimalSpecies from "../models/AnimalSpecies";
import Genders from "../models/Genders";

export async function getAnimals(axios: AxiosInstance): Promise<Animal[]> {
  const result = await axios.get("/animals").catch(e => {
    throw e.response;
  });
  const { data } = result;
  const { animalList } = data;
  return animalList;
}
