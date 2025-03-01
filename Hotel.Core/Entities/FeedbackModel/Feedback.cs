using Hotel.Core.Entities.customer;
using System;

namespace Hotel.Core.Entities.FeedbackModel
{
    public class Feedback : BaseEntity
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Customer Customer { get; set; }
        public Reservation Reservation { get; set; }
    }
}
