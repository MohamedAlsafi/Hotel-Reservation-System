using Hotel.Core.Entities.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Data.Configuration
{
    public class RoomFacilities : IEntityTypeConfiguration<RoomFacility>
    {
        public void Configure(EntityTypeBuilder<RoomFacility> builder)
        {
            builder.HasKey(rf => new { rf.RoomId, rf.FacilityId });
        }
    }
}
