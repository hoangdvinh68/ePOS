export interface ISignInRequest {
  email: string;
  password: string;
}

export interface ISignInResponse {
  accessToken: string;
  refreshToken: string;
}

export interface IGetProfileResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}
