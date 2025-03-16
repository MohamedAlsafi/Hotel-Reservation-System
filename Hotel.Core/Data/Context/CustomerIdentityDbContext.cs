using Hotel.Core.Entities.CustomerEntities;
using Hotel.Core.Entities.Enum.HotelStaff;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Hotel.Core.Data.Context
{
    public class CustomerIdentityDbContext : IdentityDbContext<Customer> 
    {
        public DbSet<Room> Room { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<RoomFacility> RoomFacility { get; set; }
        public DbSet<RoomImage> RoomImage { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<RoomOffer> RoomOffer { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<HotelStaff> HotelStaff { get; set; }
        public DbSet<RoomStaff> RoomStaff { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerData> CustomerData { get; set; }
        public CustomerIdentityDbContext(DbContextOptions<CustomerIdentityDbContext> options) : base(options)
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

            //modelBuilder.Entity<RoomOffer>().HasKey(s => new {s.OfferId , s.RoomId});
            //modelBuilder.Entity<RoomStaff>().HasKey(s => new { s.StaffId, s.RoomId });
        }
    }
}
