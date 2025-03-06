using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Reservation
{
    public class UpdateReservationDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }  
        public string? PaymenyIntentId { get; set; }
        public int GuestId { get; set; }
    }
}
