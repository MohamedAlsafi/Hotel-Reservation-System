using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Data.Configuration
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public ApiResponse(bool success, string message, T data, ErrorCode errorCode = ErrorCode.None)
        {
            Success = success;
            Message = message;
            Data = data;
            ErrorCode = errorCode;
        }

        public static ApiResponse<T> SuccessResult(T data, string message = "")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> ErrorResult(string message, ErrorCode errorCode = ErrorCode.GenericError)
        {
            return new ApiResponse<T>(false, message, default, errorCode);
        }
    }
}
