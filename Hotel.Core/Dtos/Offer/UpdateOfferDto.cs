using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.Offer
{
    public class UpdateOfferDto
    {
        public string Name { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

