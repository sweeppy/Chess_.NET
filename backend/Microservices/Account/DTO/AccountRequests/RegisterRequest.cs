namespace Account.DTO.AccountRequests
{
    public record RegisterDtoRequest(string Email, string HashedPassword);
}