using Hotel.Core.Entities.OfferModel;

namespace Hotel.Core.Entities.Rooms
{
    public class RoomOffer
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
