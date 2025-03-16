using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.CustomerEntities;
using Hotel.Core.Entities.Enum.HotelStaff;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


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
        public DbSet<CustomerData> Customer { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HotelInvoice> HotelInvoice { get; set; }
        public DbSet<HotelService> HotelService { get; set; }
        public CustomerIdentityDbContext(DbContextOptions<CustomerIdentityDbContext> options) : base(options)
        {
        }
    }
}
