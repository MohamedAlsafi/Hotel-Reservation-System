using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Dtos.Room
{
    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; } = RoomType.Single;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Facilities { get; set; } = new List<string>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
