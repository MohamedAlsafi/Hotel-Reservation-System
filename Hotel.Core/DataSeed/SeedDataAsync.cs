using Hotel.Core.Data.Context;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.DataSeed
{
    public static class SeedDataAsync
    {
        public static async Task SeedAsync(HotelDbContext dbContext)
        {
            // Seed Room Data
            // Seed RoomFacility Data
            // Seed RoomImage Data
            // Seed RoomStaff Data
            // Seed Reservation Data
            // Seed RoomOffer Data
            if (!dbContext.Rooms.Any())
            {
                var room = new Room()
                {
                    CreatedAt = DateTime.Now,
                    RoomNumber = "101",
                    Type = RoomType.Single,
                    Price = 100,
                    IsAvailable = true,
                    RoomFacilities = new List<RoomFacility>()
                    {
                        new RoomFacility()
                        {
                            FacilityId = 1
                        },
                        new RoomFacility()
                        {
                            FacilityId = 2
                        }
                    },

                    Images = new List<RoomImage>()
                    {
                        new RoomImage()
                        {
                            ImageUrl = "https://www.google.com"
                        }
                    },

                    RoomStaff = new List<RoomStaff>()
                    {
                        new RoomStaff()
                        {
                            StaffId = 1
                        }
                    },

                    Offers = new List<RoomOffer>(),
                    Reservations = new List<Reservation>()

                }; 
                await dbContext.Rooms.AddAsync(room);
                await dbContext.SaveChangesAsync();
            }

            //if (!dbContext.RoomFacilities.Any())
            //{
            //    var roomFacility = new RoomFacility()
            //    {
            //        RoomId = 1,
            //        FacilityId = 1
            //    };
            //    await dbContext.RoomFacilities.AddAsync(roomFacility);
            //    await dbContext.SaveChangesAsync();
            //}
            if (!dbContext.RoomImages.Any())
            {
                var roomImage = new RoomImage()
                {
                    RoomId = 1,
                    ImageUrl = "https://www.google.com"
                };
                await dbContext.RoomImages.AddAsync(roomImage);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.RoomStaffs.Any())
            {
                var roomStaff = new RoomStaff()
                {
                    RoomId = 1,
                    StaffId = 1
                };
                await dbContext.RoomStaffs.AddAsync(roomStaff);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Reservations.Any())
            {
                var reservation = new Reservation()
                {
                    RoomId = 1,
                    CustomerId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(2),
                    PaymentStatus = PaymentStatus.Pending,
                };
                await dbContext.Reservations.AddAsync(reservation);
                await dbContext.SaveChangesAsync();
            }

       
        }

    }
}
