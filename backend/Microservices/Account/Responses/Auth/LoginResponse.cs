using System.Text.Json.Serialization;

namespace Account.Responses.Auth
{
    public class LoginResponse(bool isSuccess, string message, string jwtToken) 
        : BaseResponse(isSuccess, message)
    {
        [JsonPropertyName("jwtToken")]
        public string JwtToken { get; set; } = jwtToken;
    }
}