using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Room.Update
{
    public class UpdateRoomImagesDto
    {
        public int RoomId { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<string> ExistingImageUrls { get; set; } = new List<string>();
    }
}
