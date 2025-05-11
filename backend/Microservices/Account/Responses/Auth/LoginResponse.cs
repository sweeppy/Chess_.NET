using System.Text.Json.Serialization;

namespace Account.Responses.Auth
{
    public class LoginResponse(bool isSuccess, string message, string jwtToken, int userId)
        : BaseResponse(isSuccess, message)
    {
        [JsonPropertyName("jwtToken")]
        public string JwtToken { get; set; } = jwtToken;
        public int UserId { get; set; } = userId;
    }
}