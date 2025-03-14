using Hotel.Core.Entities.OfferModel;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Core.Entities.Rooms
{
    public class RoomOffer
    {
        [Key]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
