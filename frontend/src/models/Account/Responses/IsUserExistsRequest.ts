import { BaseResponse } from "./BaseResponse";

export interface IsUserExistsAndEmailConfirmedResponse extends BaseResponse {
  isExists: boolean;
  isConfirmed: boolean;
  jwtToken: string | null;
  isAccountCreated: boolean;
}
