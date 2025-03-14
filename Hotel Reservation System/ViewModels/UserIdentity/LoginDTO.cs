﻿using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.ViewModels.UserIdentity
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
