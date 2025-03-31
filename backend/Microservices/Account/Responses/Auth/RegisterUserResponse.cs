namespace Account.Responses.Auth
{
    public class RegisterUserResponse : BaseResponse
    {
        public RegisterUserResponse(bool isSuccess, string message, bool isEmailSent) 
            : base(isSuccess, message)
        {
            IsEmailSent = isEmailSent;
        }
        public bool IsEmailSent;
    }
}