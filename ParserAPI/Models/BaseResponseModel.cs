namespace ParserAPI.Models;

internal class BaseResponseModel
{
    public string Message { get; set; }

    public BaseResponseModel(string Message)
    {
        this.Message = Message;
    }
}