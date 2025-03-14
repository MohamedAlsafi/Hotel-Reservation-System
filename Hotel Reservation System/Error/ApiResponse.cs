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

        private string? GetDefaultMessageForStatusCode(int statusCode) 
        {
            return statusCode switch
            {

                200 => "Ok",
                204 => "No Content",
                400 => "BadRequest",
                401 => "Unauthorized",
                404 => "Resources Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }

}
