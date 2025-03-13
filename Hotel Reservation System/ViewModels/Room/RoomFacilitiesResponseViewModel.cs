namespace Hotel_Reservation_System.ViewModels.Room
{
    public class RoomFacilitiesResponseViewModel
    {
        public int RoomId { get; set; }
        public List<string> Facilities { get; set; } = new List<string>();
    }
}
