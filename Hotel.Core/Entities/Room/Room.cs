using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Entities.Enum;

namespace Hotel.Core.Entities.Room
{
    public class Room : BaseEntity
    {

        public string RoomNumber { get; set; } 
        public RoomType Type { get; set; }
        public FacilityType Facilities { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
        public ICollection<RoomImage> Images { get; set; }

    }
}
