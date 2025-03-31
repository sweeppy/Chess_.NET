import { BaseResponse } from './BaseResponse';

export interface IsUserExistsAndEmailConfirmedResponse extends BaseResponse {
    isExists: boolean;
    isConfirmed: boolean;
    isAccountCreated: boolean;
}
