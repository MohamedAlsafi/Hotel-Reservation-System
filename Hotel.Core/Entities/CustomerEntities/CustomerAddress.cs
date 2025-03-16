using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.CustomerEntities
{
    public class CustomerAddress 
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
