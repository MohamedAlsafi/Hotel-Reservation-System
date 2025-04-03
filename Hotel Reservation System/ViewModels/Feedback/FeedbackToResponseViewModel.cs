using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.ViewModels.Feedback
{
    public class FeedbackToResponseViewModel
    {
        [Required]
        public int Id { get; set; }
        public int StaffId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Response { get; set; }

    }
}
