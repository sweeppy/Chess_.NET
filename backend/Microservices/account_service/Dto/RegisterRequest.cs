namespace account_service.Dto
{
    public record RegisterDtoRequest(string Email, string HashedPassword);
}