using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Entities.HotelStaff;

namespace Hotel.Core.Entities.Offer
{
    public class Offer:BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int CreatedBy { get; set; }
        //public HotelStaff? CreatedByStaff { get; set; }
        //public ICollection<RoomOffer>? RoomOffers { get; set; }
    }
}
