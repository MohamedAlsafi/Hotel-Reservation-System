using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Entities.Enum;

namespace Hotel.Core.Entities.Rooms
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; } 
        public RoomType Type { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
        public ICollection<RoomFacility> RoomFacilities { get; set; } = new List<RoomFacility>();
        public ICollection<RoomImage> Images { get; set; } = new List<RoomImage>();
        public ICollection<RoomStaff> RoomStaff { get; set; } = new HashSet<RoomStaff>();
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public ICollection<RoomOffer> Offers { get; set; } = new HashSet<RoomOffer>();
    }
}
