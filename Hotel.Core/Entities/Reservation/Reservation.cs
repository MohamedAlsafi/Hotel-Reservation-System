﻿using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Reservation
{
    public class Reservation : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public Feedback Feedback { get; set; }

    }
}
