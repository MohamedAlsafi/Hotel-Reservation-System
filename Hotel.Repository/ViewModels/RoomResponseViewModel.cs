namespace Hotel_Reservation_System.ViewModels
{
    public class RoomResponseViewModel
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Facilities { get; set; } = new List<string>(); 
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
