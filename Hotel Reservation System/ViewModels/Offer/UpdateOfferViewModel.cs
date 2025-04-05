namespace Hotel_Reservation_System.ViewModels.Offer
{
    public class UpdateOfferViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> RoomIds { get; set; } = new List<int>();
    }
}
