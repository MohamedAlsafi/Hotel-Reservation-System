using Hotel.Core.Entities.HotelStaffs;
using Hotel.Core.Entities.Rooms;
using System;
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
        public HotelStaff? CreatedByStaff { get; set; }
        public ICollection<RoomOffer>? RoomOffers { get; set; } = new List<RoomOffer>();
        public bool IsRoomAvailable { get; set; }

    }
}
