namespace Account.DTO.AccountRequests
{
    public record CreateAccountRequest(IFormFile profileImage, string Username, string Password,
    string ConfirmPassword);
}