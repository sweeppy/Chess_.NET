namespace Account.Responses
{
    public class BaseResponse
    {
        public BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            ResponseMessage = message;
        }
        public bool IsSuccess { get; set; }

        public string ResponseMessage { get; set; }
    }
}