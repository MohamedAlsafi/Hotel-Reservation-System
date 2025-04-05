using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Offer
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> AssignedRoomIds { get; set; } = new List<int>();
    }
}
