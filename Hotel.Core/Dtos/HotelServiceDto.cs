using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos
{
   public class HotelServiceDto
    {
        public int Id { get; set; } // Primary Key
        public string Description { get; set; }
        public decimal Amount { get; set; }

        // Foreign Key
        public int HotelInvoiceId { get; set; }
    }
}
