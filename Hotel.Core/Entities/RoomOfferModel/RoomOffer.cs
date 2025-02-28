using Hotel.Core.Entities.OfferModel;

namespace Hotel.Core.Entities.RoomOfferModel
{
    public class RoomOffer
    {
        public int RoomId { get; set; }
        public Room.Room Room { get; set; } 

        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
