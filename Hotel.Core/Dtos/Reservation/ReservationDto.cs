using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } 
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; } = 0;
        public string? PaymentIntentId { get; set; }
        public List<string> PictureUrl { get; set; } = new List<string>();
        public string PaymentStatus { get; set; }

    }
}
