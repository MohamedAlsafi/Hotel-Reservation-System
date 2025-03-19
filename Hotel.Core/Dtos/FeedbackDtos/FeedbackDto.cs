using Hotel.Core.Entities.Enum;

namespace Hotel.Core.Dtos.FeedbackDtos
{
    public class FeedbackDto
    {
        public int? CustomerId { get; set; }
        public int ReservationId { get; set; }
        public CustomerFeedback Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
        public new DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Response { get; set; }
    }
}
