using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Dtos.FeedbackDtos
{
    public class AddFeedbackDto
    {
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }  // 1 to 5 scale
        public string? Comment { get; set; }

    }
}
