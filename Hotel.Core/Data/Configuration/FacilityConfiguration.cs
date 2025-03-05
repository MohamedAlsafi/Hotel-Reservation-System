using Hotel.Core.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Data.Configuration
{
    public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasData(
                new Facility { Id = 1, Name = "Wifi" },
                new Facility { Id = 2, Name = "TV" },
                new Facility { Id = 3, Name = "Mini Bar" },
                new Facility { Id = 4, Name = "air conditioning" });

        }
    }
}
