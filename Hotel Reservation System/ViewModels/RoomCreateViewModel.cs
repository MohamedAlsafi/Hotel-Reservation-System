namespace Hotel_Reservation_System.ViewModels
{
    public class RoomCreateViewModel
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> FacilityIds { get; set; } = new List<int>(); 
        public string ImageUrl { get; set; } = string.Empty;

    }
}
