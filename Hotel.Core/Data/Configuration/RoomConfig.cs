using Hotel.Core.Entities.Rooms;
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
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            //  Store RoomType as text in database
            builder.Property(r => r.Type)
                .HasConversion<string>();

            builder.Property(r => r.Price)
             .HasColumnType("decimal(10,2)")
             .IsRequired();

            builder.HasMany(r => r.Images)
            .WithOne(img => img.Room)
            .HasForeignKey(img => img.RoomId);


        }
    }
}
