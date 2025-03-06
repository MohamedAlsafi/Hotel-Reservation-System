using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Offer
{
    public class CreateOfferDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> AssignedRoomIds { get; set; } = new List<int>();
    }
}
