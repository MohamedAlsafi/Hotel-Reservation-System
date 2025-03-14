using Hotel.Core.Data.Context;
using Hotel.Core.Dtos;
using Hotel.Core.Entities.Enum;
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
        private readonly HotelDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly UserManager<HotelStaff> _userManager;

        public HotelStaffController(HotelDbContext dbContext , ITokenService tokenService , UserManager<HotelStaff> userManager)
        {
            this._dbContext = dbContext;
            this._tokenService = tokenService;
            this._userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<HotelStaffDTO>> Register(RegisterStaffDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest(new ApiExcaptionResponse(400, "Email already exists"));

            var User = new HotelStaff()
            { Address = model.Address,
                Email = model.Email, FirstName = model.FirstName,
                LastName = model.LastName, Password = model.Password,
                PhoneNumber = model.PhoneNumber, 
               Role = (HotelStaffRole)Enum.Parse(typeof(HotelStaffRole), model.Role)
            };
            if(User is null) return BadRequest(new ApiExcaptionResponse(400));
            var result = await _userManager.CreateAsync(User, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiExcaptionResponse(400));
            var ResultDto = new HotelStaffDTO()
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                Token = await _tokenService.GetTokenAsyncForHotelStaff(User, _userManager)
            };
            return Ok(ResultDto);

        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var user = await _dbContext.HotelStaffs.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if(user is null) return Unauthorized(new ApiExcaptionResponse(401));
            var ResultDto = new HotelStaffDTO()
            {
                Email = user.Email,
                UserName = user.Email.Split('@')[0],
                Token = await _tokenService.GetTokenAsyncForHotelStaff(user, _userManager)
            };

            return Ok(ResultDto);
        }
    }
}
