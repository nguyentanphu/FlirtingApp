export interface ISignUpModel {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
  dateOfBirth: Date;
  gender: Gender;
}

export enum Gender {
  Unknown = 0,
  Male = 1,
  Female = 2,
  Trans = 3
}
