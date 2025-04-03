using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.CustomerEntities;
using Hotel.Core.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Core.Entities.FeedbackModel
{
    public class Feedback : BaseEntity
    {
        public int? CustomerId { get; set; }
        public int ReservationId { get; set; }
        public CustomerFeedback Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }

        public string? Response { get; set; } = null;

        public DateTime? ResponseDate { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation.Reservation Reservation { get; set; }
    }
}
