using Hotel.Core.Entities.CustomerEntities;
using System;

namespace Hotel.Core.Entities.FeedbackModel
{
    public class Feedback : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
        public new DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Customer Customer { get; set; } = new Customer();
        public Reservation.Reservation Reservation { get; set; }
    }
}
