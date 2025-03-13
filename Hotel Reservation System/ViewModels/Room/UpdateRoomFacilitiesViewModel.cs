namespace Hotel_Reservation_System.ViewModels.Room
{
    public class UpdateRoomFacilitiesViewModel
    {
        public int RoomId { get; set; }
        public List<int> FacilityIds { get; set; } = new List<int>();
    }
}
