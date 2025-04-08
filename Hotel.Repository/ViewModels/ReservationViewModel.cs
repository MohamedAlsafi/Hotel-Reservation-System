using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public string? PaymenyIntentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal TotalPrice { get; set; } 
    }
}
