using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.CustomerEntities;

namespace Hotel.Core.Entities.FeedbackModel
{
    public class Feedback : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }
        public new DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CustomerData Customer { get; set; } 
        public Reservation.Reservation Reservation { get; set; }
    }
}
