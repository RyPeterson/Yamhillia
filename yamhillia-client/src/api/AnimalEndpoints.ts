import { AxiosInstance } from "axios";
import shuffle from "lodash/shuffle";
import Animal from "../models/Animal";
import AnimalSpecies from "../models/AnimalSpecies";
import Genders from "../models/Genders";

function generateAnimals(count: number): Animal[] {
  const animals: Animal[] = [];
  for (let i = 0; i < count; ) {
    const speciesValues = Object.values(AnimalSpecies);
    for (const species of speciesValues) {
      const genders = Object.values(Genders);
      for (const gender of genders) {
        animals.push({
          id: (i + 1).toString(),
          name: `${species} ${gender}_${i}`,
          species,
          gender,
          image: null
        });
        i += 1;
      }
    }
  }
  return shuffle(animals);
}

const mockAnimals: Animal[] = generateAnimals(123);

export async function getAnimals(axios: AxiosInstance): Promise<Animal[]> {
  return Promise.resolve(mockAnimals);
}
