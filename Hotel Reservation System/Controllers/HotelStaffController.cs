using Hotel.Core.Data.Context;
using Hotel.Core.Dtos;
using Hotel.Core.Entities.Enum.HotelStaff;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelStaffController : ControllerBase
    {
        private readonly CustomerIdentityDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly UserManager<HotelStaff> _userManager;

        public HotelStaffController(CustomerIdentityDbContext dbContext , ITokenService tokenService , UserManager<HotelStaff> userManager)
        {
            this._dbContext = dbContext;
            this._tokenService = tokenService;
            this._userManager = userManager;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(HotelStaffDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var user = await _dbContext.HotelStaff.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if(user is null) return Unauthorized(new ApiExcaptionResponse(401));
            var ResultDto = new HotelStaffDTO()
            {
                Email = user.Email,
                UserName = user.Email.Split('@')[0],
               // Token = await _tokenService.GetTokenAsync(user, _userManager)
            };

            return Ok(ResultDto);
        }
    }
}
