import Model from "./Model";
import AnimalSpecies from "./AnimalSpecies";
import Genders from "./Genders";

export default interface Animal extends Model {
  name: string;
  species: AnimalSpecies;
  gender: Genders;
  image: string | null;
  customIdentifier: string | null;
  dateOfBirth: string | null;
  dateOfDeath: string | null;
}
