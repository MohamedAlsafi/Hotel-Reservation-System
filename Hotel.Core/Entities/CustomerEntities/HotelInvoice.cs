﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.CustomerEntities
{
    public class HotelInvoice
    {
        public int Id { get; set; } 
        public string InvoiceNumber { get; set; }
        public string GuestName { get; set; }
        public string ContactDetails { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomType { get; set; }
        public int NumberOfNights { get; set; }
        public decimal NightlyRate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
        public ICollection<HotelService> AdditionalServices { get; set; }
    }
}
