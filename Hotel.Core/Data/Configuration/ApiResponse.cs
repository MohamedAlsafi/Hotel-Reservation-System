using System.Text.Json.Serialization;
using Hotel.Core.Entities.Enum;

public class ResponseViewModel<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // Hides null values
    public ErrorCode? ErrorCode { get; set; }

    public ResponseViewModel(bool success, string? message, T data, ErrorCode? errorCode = null)
    {
        Success = success;
        Message = message;
        Data = data;
        ErrorCode = errorCode;
    }

    public static ResponseViewModel<T> SuccessResult(T data, string message = "")
    {
        return new ResponseViewModel<T>(true, message, data, null); // No error code for success
    }

    public static ResponseViewModel<T> ErrorResult(string message, ErrorCode errorCode)
    {
        return new ResponseViewModel<T>(false, message, default, errorCode); // Includes error code
    }
}
