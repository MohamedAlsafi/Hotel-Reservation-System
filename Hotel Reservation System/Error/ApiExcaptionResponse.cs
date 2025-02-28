namespace Hotel_Reservation_System.Error
{
    public class ApiExcaptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExcaptionResponse(int statuscode, string message = null, string details=null ):base(statuscode,message)
        {
            Details = details;
             
        }
    }
}
