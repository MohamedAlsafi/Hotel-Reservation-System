using AutoMapper;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Entities.Rooms;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomDTO = Hotel.Core.Dtos.Room.RoomDTO;

namespace Hotel.Core.Helpers
{
    public class PictureUrlResolver : IValueResolver<RoomImage, RoomDTO,string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(RoomImage source, RoomDTO destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.ImageUrl))
                return $"{_configuration["ApiUrl"]}{source.ImageUrl}";
            return string.Empty;
        }
    }
}
