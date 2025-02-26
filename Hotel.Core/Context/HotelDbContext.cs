using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Context
{
    public class HotelDbContext :DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) :base(options) 
        {
            
        }
    }
}
