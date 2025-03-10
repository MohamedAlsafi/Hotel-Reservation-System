using Hotel_Reservation_System.ViewModels;

namespace Hotel.Core.Dtos.Reservation
{
    public class CustomerReservationViewModel
    {
        public CreateReservationDto Reservation { get; set; } = new();

        public PaymentProcessViewModel Payment { get; set; }
    }
}
