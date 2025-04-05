using Hotel.Core.Entities.HotelStaffs;

namespace Hotel_Reservation_System.ViewModels
{
    public class CreateOfferViewModel
    {
        public string Title { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> AssignedRoomIds { get; set; } = new List<int>();
        public int? CreatedByStaff { get; set; }

    }
}
