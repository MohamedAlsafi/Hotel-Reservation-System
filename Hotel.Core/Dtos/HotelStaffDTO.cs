using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos
{
    public class HotelStaffDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public string Role { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
