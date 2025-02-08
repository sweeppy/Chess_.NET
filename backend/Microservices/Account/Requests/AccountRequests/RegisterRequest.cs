namespace Account.DTO.Requests.AccountRequests
{
    public record RegisterDtoRequest(string Email, string HashedPassword);
}