using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Offer
{
    public class OfferListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ValidUntil { get; set; } 
    }
}
