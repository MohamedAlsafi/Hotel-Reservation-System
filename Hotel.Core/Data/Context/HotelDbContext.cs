using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Room;
using Hotel.Core.Entities.RoomOfferModel;
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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RoomOffer> RoomOffers { get; set; }
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var foreignKeys = entityType.GetForeignKeys();
                foreach (var fk in foreignKeys)
                {
                    fk.DeleteBehavior = DeleteBehavior.NoAction;
                }
            }
        }

    }
}
