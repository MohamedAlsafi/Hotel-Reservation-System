namespace Hotel_Reservation_System.Error
{
    public class ApiValidathionErrorr : ApiResponse
    {

        public IEnumerable<string> Errors { get; set; }

        public ApiValidathionErrorr():base (400)
        {
             Errors = new List<string>();
        }
    }
}
