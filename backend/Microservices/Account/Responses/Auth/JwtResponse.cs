namespace Account.Responses.Auth
{
    public class JwtResponse : BaseResponse
    {
        public JwtResponse(bool isSuccess, string message, string accessToken, string refreshToken) : base(isSuccess, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}