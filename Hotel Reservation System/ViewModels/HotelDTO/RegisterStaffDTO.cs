using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.HotelDTO
{
   public class RegisterStaffDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } 
    }
}
