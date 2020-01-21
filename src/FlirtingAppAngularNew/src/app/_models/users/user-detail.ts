import { IPhotoDto } from '../photos/photo-dto';

export interface IUserDetail {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  age: number;
  knownAs: string;
  created: Date;
  lastActive: Date;
  introduction: string;
  lookingFor: string;
  interests: string;
  city: string;
  country: string;
  photoUrl: string;
  photos: IPhotoDto[];
}
