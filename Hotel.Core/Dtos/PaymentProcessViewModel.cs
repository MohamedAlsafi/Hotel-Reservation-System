using Hotel.Core.Entities.Enum;

namespace Hotel_Reservation_System.ViewModels
{
    public class PaymentProcessViewModel
    {
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; } = 0m;
        public string?PaymentIntentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;





    }
}
