namespace Account.Responses.Auth
{
    public class LoginResponse : BaseResponse
    {
        public bool IsEmailSent;
        public LoginResponse(bool isSuccess, string message, bool isEmailSent) : base(isSuccess, message)
        {
            IsEmailSent = isEmailSent;
        }
    }
}