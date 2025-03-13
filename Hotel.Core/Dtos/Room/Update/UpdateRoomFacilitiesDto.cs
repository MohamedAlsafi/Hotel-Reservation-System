using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Room.Update
{
    public class UpdateRoomFacilitiesDto
    {
        public int RoomId { get; set; }
        public List<int> FacilityIds { get; set; } = new List<int>();
    }
}
