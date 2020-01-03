import { Gender } from './sign-up-model';

export interface IMemberListFilter {
  gender?: Gender;
  coordinates?: number[];
  distance: number;
}

