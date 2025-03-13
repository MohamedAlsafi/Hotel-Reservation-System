using Hotel.Core.Entities.Enum;

namespace Hotel_Reservation_System.ViewModels.Room
{
    public class RoomCreateViewModel
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public List<int> FacilityIds { get; set; } = new List<int>();
        public List<IFormFile> Images { get; set; }
    }
}
