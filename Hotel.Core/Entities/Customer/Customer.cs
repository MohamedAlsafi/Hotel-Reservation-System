using Hotel.Core.Entities.FeedbackModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.customer
{
    public class Customer : IdentityUser
    {
     
        public new int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
      

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public ICollection<Reservation.Reservation> Reservations { get; set; } = new HashSet<Reservation.Reservation>();
    }
}
