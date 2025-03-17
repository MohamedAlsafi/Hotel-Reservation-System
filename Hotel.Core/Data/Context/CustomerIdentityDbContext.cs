using Hotel.Core.Entities.customer;
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
        public DbSet<CustomerData> CustomerDataSet { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<HotelInvoice> HotelInvoice { get; set; }
        public DbSet<HotelService> HotelService { get; set; }
        public CustomerIdentityDbContext(DbContextOptions<CustomerIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // Customer and Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room and Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure RoomFacility as a many-to-many join entity
            modelBuilder.Entity<RoomFacility>()
                .HasKey(rf => new { rf.RoomId, rf.FacilityId }); // Use composite key

            modelBuilder.Entity<RoomFacility>()
                .HasOne(rf => rf.Room)
                .WithMany(r => r.RoomFacilities)
                .HasForeignKey(rf => rf.RoomId);

            modelBuilder.Entity<RoomFacility>()
                .HasOne(rf => rf.Facility)
                .WithMany(f => f.RoomFacilities)
                .HasForeignKey(rf => rf.FacilityId);

            modelBuilder.Entity<RoomOffer>()
                .HasOne(ro => ro.Room)
                .WithMany(r => r.Offers)
                .HasForeignKey(ro => ro.RoomId);

            modelBuilder.Entity<RoomStaff>()
                .HasOne(rs => rs.Room)
                .WithMany(r => r.RoomStaff)
                .HasForeignKey(rs => rs.RoomId);

            modelBuilder.Entity<HotelInvoice>()
            .Property(h => h.NightlyRate)
            .HasPrecision(18, 4); // تحديد الدقة

            modelBuilder.Entity<HotelInvoice>()
                .Property(h => h.SubTotal)
                .HasPrecision(18, 4);

            modelBuilder.Entity<HotelInvoice>()
                .Property(h => h.Tax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<HotelInvoice>()
                .Property(h => h.TotalAmount)
                .HasPrecision(18, 4);


        }
    }

}


