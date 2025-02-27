using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Room
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; } 
    }
}
