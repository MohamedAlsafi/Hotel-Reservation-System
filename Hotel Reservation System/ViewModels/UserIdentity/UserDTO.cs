using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.ViewModels.UserIdentity
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
