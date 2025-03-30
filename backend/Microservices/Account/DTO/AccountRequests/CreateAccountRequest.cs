namespace Account.DTO.AccountRequests
{
    public record CreateAccountRequest(IFormFile ProfileImage, string Username, string Password,
    string ConfirmPassword);
}