using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Room.Update
{
    public class UpdateRoomImagesResponseDto
    {
        public int Id { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
