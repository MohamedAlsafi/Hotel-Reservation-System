using Hotel.Core.Entities.Enum;

namespace Hotel_Reservation_System.ViewModels.Feedback
{
    public class AddFeedbackViewModel
    {
        public int? CustomerId { get; set; }
        public int ReservationId { get; set; }
        public CustomerFeedback Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
    }
}
