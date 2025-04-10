using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Chess.Responses.GameProcess
{
    public class GameResponse(bool isSuccess, string message,
        string fen, Dictionary<int, List<int>> legalMoves)

            : BaseResponse(isSuccess, message)
    {
        [JsonPropertyName("fen")]
        public string Fen { get; set; } = fen;
        [JsonPropertyName("legalMoves")]
        public Dictionary<int, List<int>> LegalMoves { get; set; } = legalMoves;
    }
}