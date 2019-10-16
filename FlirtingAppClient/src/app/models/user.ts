import { Photo } from './photo';

export interface User {
  id: string;
  userName: string;
  knownAs: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;
  interest?: string;
  introduction?: string;
  lookingFor?: string;
  photos: Photo[];
}
