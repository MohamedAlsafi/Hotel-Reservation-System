using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.FeedbackDtos
{
    public class FeedbackResponseDto
    {
        [Required]
        public int StaffId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Response { get; set; }
    }
}
