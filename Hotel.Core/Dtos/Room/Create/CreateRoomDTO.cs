using Hotel.Core.Entities.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Room.Create
{
    public class CreateRoomDTO
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public List<int> FacilityIds { get; set; } = new List<int>();
        public List<IFormFile> Images { get; set; }
    }
}
