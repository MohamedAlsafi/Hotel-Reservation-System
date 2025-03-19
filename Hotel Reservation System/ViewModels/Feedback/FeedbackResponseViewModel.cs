using Hotel.Core.Entities.Enum;

namespace Hotel_Reservation_System.ViewModels.Feedback
{
    public class FeedbackResponseViewModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int ReservationId { get; set; }
        public CustomerFeedback Rating { get; set; }  
        public string? Comment { get; set; }
    }
}
