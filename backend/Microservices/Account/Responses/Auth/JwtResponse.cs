namespace Account.Responses.Auth
{
    public class JwtResponse(bool isSuccess, string message, string jwtToken)
    : BaseResponse(isSuccess, message)
    {
        public string JwtToken { get; set; } = jwtToken;
    }
}