namespace Hotel_Reservation_System.Error
{
    public class ApiValidationError : ApiResponse
    {

        public IEnumerable<string> Errors { get; set; }

        public ApiValidationError():base (400)
        { 
             Errors = new List<string>();
        }
    }
}
