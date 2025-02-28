﻿using AutoMapper;
using Hotel.Core.Entities.Room;
using Hotel.Repository.Dtos.Room;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
