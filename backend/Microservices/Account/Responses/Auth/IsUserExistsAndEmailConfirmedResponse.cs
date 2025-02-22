using System.Text.Json.Serialization;

namespace Account.Responses.Auth
{
    public class IsUserExistsAndEmailConfirmedResponse(
        bool isSuccess, string message, bool isExists, bool isEmailConfirmed, string? jwtToken, bool isAccountCreated)
        : BaseResponse(isSuccess, message)
    {
        [JsonPropertyName("isExists")]
        public bool IsUserExists { get; set; } = isExists;
        [JsonPropertyName("isConfirmed")]
        public bool IsEmailConfirmed { get; set; } = isEmailConfirmed;
        [JsonPropertyName("jwtToken")]
        public string? JwtToken { get; set; } = jwtToken;
        [JsonPropertyName("isAccountCreated")]
        public bool IsAccountCreated { get; set; } = isAccountCreated;
    }
}