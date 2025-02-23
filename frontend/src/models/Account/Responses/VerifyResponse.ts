import { BaseResponse } from "./BaseResponse";

export interface VerifyResponse extends BaseResponse {
  isCodeCorrect: boolean;
  jwtToken: string | null;
}
