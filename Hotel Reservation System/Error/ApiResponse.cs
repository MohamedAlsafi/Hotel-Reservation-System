using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Hotel_Reservation_System.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massege { get; set; }

        public ApiResponse(int stauscode , string? massege=null)
        {
            StatusCode = stauscode;
            Massege = massege ?? GetDefaultMessageForStatusCode(stauscode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode) 
        {
            return statusCode switch
            {
                400 => "A Bad Request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.Hate leads to career thange",
                _ => null

            };
        }
    }

}
