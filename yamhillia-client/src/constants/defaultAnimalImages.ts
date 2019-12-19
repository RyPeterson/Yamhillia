import AnimalSpecies from "../models/AnimalSpecies";
import {
  chickenPlaceholder,
  cowPlaceholder,
  duckPlaceholder,
  goatPlaceholder,
  horsePlaceholder,
  llamaPlaceholder,
  pigPlaceholder,
  rabbitPlaceholder,
  turkeyPlaceholder,
  goosePlaceholder
} from "./images";

type SpeciesEnumKey = {
  [key in AnimalSpecies]: string;
};
const defaultAnimalPlaceHolders: SpeciesEnumKey = {
  [AnimalSpecies.CHICKEN]: chickenPlaceholder,
  [AnimalSpecies.COW]: cowPlaceholder,
  [AnimalSpecies.DUCK]: duckPlaceholder,
  [AnimalSpecies.GOAT]: goatPlaceholder,
  [AnimalSpecies.GOOSE]: goosePlaceholder,
  [AnimalSpecies.HORSE]: horsePlaceholder,
  [AnimalSpecies.LLAMA]: llamaPlaceholder,
  [AnimalSpecies.PIG]: pigPlaceholder,
  [AnimalSpecies.RABBIT]: rabbitPlaceholder,
  [AnimalSpecies.TURKEY]: turkeyPlaceholder
};

export default defaultAnimalPlaceHolders;
