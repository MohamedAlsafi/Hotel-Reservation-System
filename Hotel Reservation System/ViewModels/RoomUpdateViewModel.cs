namespace Hotel_Reservation_System.ViewModels
{
    public class RoomUpdateViewModel
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> FacilityIds { get; set; } = new List<int>();
        public string ImageUrl { get; set; } = string.Empty;
    }
}
