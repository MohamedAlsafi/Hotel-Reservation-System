using Hotel.Core.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.HotelStaff
{
    public class HotelStaff :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<RoomStaff> RoomStaff { get; set; }

    }
}
