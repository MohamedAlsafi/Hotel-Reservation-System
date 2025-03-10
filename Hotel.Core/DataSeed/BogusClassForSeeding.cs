using Bogus;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Entities.Rooms;
using System;



namespace Hotel.Core.DataSeed
{
    public class BogusClassForSeeding 
    {
        private readonly Faker<RoomDTO> _roomDataFaker;

        public BogusClassForSeeding(Faker<Room> RoomDataFaker)
        {
            _roomDataFaker = new Faker<RoomDTO>()
                .RuleFor(r => r.RoomNumber, f => f.Random.String2(5))
                .RuleFor(r => r.ImageUrl, opt => opt.Image.ToString());

        }
        

    }
}
