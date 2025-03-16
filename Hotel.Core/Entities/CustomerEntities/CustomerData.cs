using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.CustomerEntities
{
     public  class CustomerData 
     {
       
            [Key]
            public int Id { get; set; }

            [Required, StringLength(50)]
            public required string FirstName { get; set; }

            [Required, StringLength(50)]
            public required string LastName { get; set; }

            public string? Email { get; set; }

            [Phone]
            public string? PhoneNumber { get; set; }

            public ICollection<Reservation.Reservation> Reservations { get; set; } = new HashSet<Reservation.Reservation>();
        


    }
}
