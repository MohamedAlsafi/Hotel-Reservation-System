using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.HotelStaff;
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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RoomOffer> RoomOffers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<HotelStaff> HotelStaffs { get; set; }
        public DbSet<RoomStaff> RoomStaffs { get; set; }

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

            modelBuilder.Entity<RoomOffer>().HasKey(s => new {s.OfferId , s.RoomId});
            modelBuilder.Entity<RoomStaff>().HasKey(s => new { s.StaffId, s.RoomId });
        }

    }
}
