using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum.HotelStaff;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Data.Context
{
    public class HotelDbContext : DbContext
    {


        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {

        }

    }
}
