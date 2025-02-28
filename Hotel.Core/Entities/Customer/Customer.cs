using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Customer
{
    public class Customer:IdentityUser
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required,StringLength(50)]
        public string LastName { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        public  CustomerAddress Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }


    }
}
