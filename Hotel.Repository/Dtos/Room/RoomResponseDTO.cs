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
        public string RoomNumber { get; set; }
        public string Type { get; set; }  
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Facilities { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
