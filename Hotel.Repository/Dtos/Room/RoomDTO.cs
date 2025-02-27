using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Dtos.Room
{
    public class RoomDTO
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; } = RoomType.Single;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> FacilityIds { get; set; }  
        public List<string> ImageUrls { get; set; }
    }
}
