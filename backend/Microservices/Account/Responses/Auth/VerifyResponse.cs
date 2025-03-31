using System.Text.Json.Serialization;

namespace Account.Responses.Auth
{
    public class VerifyResponse(bool isSuccess, string message, bool isCodeCorrect, string? jwtToken)
        : BaseResponse(isSuccess, message)
    {
        [JsonPropertyName("isCodeCorrect")]
        public bool IsCodeCorrect { get; set; } = isCodeCorrect;
        [JsonPropertyName("jwtToken")]
        public string? JwtToken { get; set; } = jwtToken;
    }
}