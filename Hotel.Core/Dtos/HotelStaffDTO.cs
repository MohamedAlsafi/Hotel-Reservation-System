using Hotel.Core.Entities.Enum;
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
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
      
        [Required]
        public string Token { get; set; }
    }
}
