using Hotel.Core.Entities.Enum;

namespace Hotel_Reservation_System.ViewModels.Room
{
    public class RoomResponseViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public List<string> Facilities { get; set; } = new List<string>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
