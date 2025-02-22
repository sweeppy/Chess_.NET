export interface IsUserExistsAndEmailConfirmedResponse {
  isSuccess: boolean;
  message: string;
  isExists: boolean;
  isConfirmed: boolean;
  jwtToken: string | null;
}
