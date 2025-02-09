namespace Account.Responses.Auth
{
    public class RegisterUserResponse : BaseResponse
    {
        public bool IsEmailSent;
        public RegisterUserResponse(bool isSuccess, string message, bool isEmailSent) : base(isSuccess, message)
        {
            IsEmailSent = isEmailSent;
        }
    }
}