namespace Hotel_Reservation_System.ViewModels.Feedback
{
    public class FeedbackResponseViewModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
    }
}
