using Hotel.Core.Entities.RoomOfferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.OfferModel
{
    public class Offer: BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public HotelStaff.HotelStaff? CreatedByStaff { get; set; }
        public ICollection<RoomOffer>? RoomOffers { get; set; } = new List<RoomOffer>();
    }
}
