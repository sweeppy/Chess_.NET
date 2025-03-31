import { BaseResponse } from './BaseResponse';

export interface LoginResponse extends BaseResponse {
    jwtToken: string;
}
