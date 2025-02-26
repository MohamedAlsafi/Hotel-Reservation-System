using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Room
{
    public class RoomImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
