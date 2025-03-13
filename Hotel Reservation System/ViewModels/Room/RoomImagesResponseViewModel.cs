namespace Hotel_Reservation_System.ViewModels.Room
{
    public class RoomImagesResponseViewModel
    {
        public int RoomId { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
