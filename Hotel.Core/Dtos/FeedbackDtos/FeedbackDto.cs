using Hotel.Core.Entities.Enum;

namespace Hotel.Core.Dtos.FeedbackDtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int ReservationId { get; set; }
        public CustomerFeedback Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
        public  DateTime CreatedAt { get; set; } = DateTime.UtcNow;     
        public DateTime? ResponseDate { get; set; }

        public string Response { get; set; }
    }
}
