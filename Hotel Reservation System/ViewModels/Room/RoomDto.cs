namespace Hotel_Reservation_System.ViewModels.Room
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Type { get; set; }
        public decimal Price { get; set; }
        public List<string> Facilities { get; set; } = new List<string>();
        public List<string> ImageUrls { get; set; } = new List<string>();

    }
}
