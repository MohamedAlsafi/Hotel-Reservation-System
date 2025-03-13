using Hotel.Core.Entities.Enum;

namespace Hotel_Reservation_System.ViewModels.Room
{
    public class UpdateRoomDetailsViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        
    }
}
