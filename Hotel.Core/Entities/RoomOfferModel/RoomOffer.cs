using Hotel.Core.Entities.OfferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
